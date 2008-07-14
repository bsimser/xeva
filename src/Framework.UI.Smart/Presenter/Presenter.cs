using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace XF.UI.Smart
{
   public abstract class Presenter<TView, TCallbacks> : IPresenter
      where TCallbacks : class, IViewCallbacks
      where TView : class, IView<TCallbacks>
   {
      private readonly Dictionary<string, IControl> _controls = new Dictionary<string, IControl>();
      private IAsyncWorker _asyncWorker;
      private bool _isFinished;
      private bool _isStarted;
      private string _key;
      private string _label;
      private IPresenterValidator _presenterValidator;
      private IRequest _request;
      private TView _view;
      private IWindowAdapter _windowAdapter;
      private IWindowRegistry _windowRegistry;

      protected bool HasStarted
      {
         get { return _isStarted; }
      }

      protected bool HasFinished
      {
         get { return _isFinished; }
      }

      public TView View
      {
         get { return _view; }
         set { _view = value; }
      }

      public Dictionary<string, IControl> Controls
      {
         get { return _controls; }
      }

      public string Key
      {
         get { return _key; }
         set { _key = value; }
      }

      public string Label
      {
         get { return _label; }
         set { _label = value; }
      }

      protected virtual void InitializeRequest(IRequest request)
      {
      }

      public void Start()
      {
         Start(new NullRequest());
      }

      public void Start(IRequest request)
      {
         _request = request;

         if (HasStarted) return;
         if (View == null) throw new ViewNotAvailableException();

         InitializeRequest(request);

         var callbacks = this as TCallbacks;
         if (callbacks == null) throw new NoCallbacksImplementationException();

         View.Attach(callbacks);

         if (View is IAsyncView<TCallbacks>)
         {
            InitializeAsyncWorker();
            ((IAsyncView<TCallbacks>) View).ShowWaiting();
            Window.Show();
            _asyncWorker.RunWorkerAsync();
         }
         else
         {
            CustomStart();
            Window.Show();
         }

         _isStarted = true;
      }

      protected virtual void CustomStart()
      {
      }

      public virtual void ResumeCustomStart()
      {
      }

      public virtual void ReInitialize(IRequest request)
      {
      }

      public void Finish()
      {
         Finish(false);
      }

      private void Finish(bool windowInitiated)
      {
         if (!HasStarted) return;
         if (HasFinished) return;
         if (!windowInitiated && (_windowAdapter != null))
         {
            _windowAdapter.Closed -= OnWindowClosed;
            _windowAdapter.Close();
         }
         _windowAdapter = null;
         CustomFinish();
         _isFinished = true;
         OnFinished(new PresenterFinishedEventArgs(Key));
      }

      protected virtual void CustomFinish()
      {
      }

      public event EventHandler<PresenterFinishedEventArgs> Finished;

      protected virtual void OnFinished(PresenterFinishedEventArgs args)
      {
         if (Finished != null)
            Finished(this, args);
      }

      public virtual object UI
      {
         get
         {
            var result = (_view == null) ? null : _view.UI;
            return result;
         }
      }

      public void DisplayIn(IWindowManager manager, IWindowOptions options)
      {
         if (UI == null) throw new NoUserInterfaceObjectException();

         _windowAdapter = manager.CreateWindowFor(this);

         if (options != null) _windowAdapter.ApplyOptions(options);
         _windowAdapter.InitializeUI(UI);
         _windowAdapter.Closed += OnWindowClosed;

         if (HasStarted) _windowAdapter.Show();
      }

      public void DisplayIn(IWindowAdapter windowAdapter)
      {
         if (UI == null) throw new NoUserInterfaceObjectException();
         _windowAdapter = windowAdapter;
         _windowAdapter.InitializeUI(UI);
         _windowAdapter.Closed += OnWindowClosed;
         if (HasStarted) _windowAdapter.Show();
      }

      public bool Validate(object target)
      {
         return Validate(new object[1] {target});
      }

      public bool Validate(object[] targets)
      {
         InitializeValidator(new PresenterValidator());

         return _presenterValidator.Validate(targets, _controls);
      }

      public virtual void InitializeValidator(IPresenterValidator presenterValidator)
      {
         if (_presenterValidator == null)
            _presenterValidator = presenterValidator;
      }

      public IWindowController Window
      {
         get { return (IWindowController) _windowAdapter ?? new NoWindowControls(); }
      }

      public void RegisterControl(string property, IControl control)
      {
         _controls.Add(property, control);
      }

      private void InitializeAsyncWorker()
      {
         if (_asyncWorker == null)
         {
            _asyncWorker = Locator.Resolve<IAsyncWorker>();
            _asyncWorker.DoWork += AsyncCustomStart;
            _asyncWorker.RunWorkerCompleted += OnAsyncWorkerComplete;
         }
      }

      private void AsyncCustomStart(object sender, DoWorkEventArgs e)
      {
         CustomStart();
      }

      private void OnAsyncWorkerComplete(object sender, RunWorkerCompletedEventArgs e)
      {
         ((IAsyncView<TCallbacks>) View).HideWaiting();
         ResumeCustomStart();
      }

      private void OnWindowClosed(object sender, EventArgs e)
      {
         Finish(true);
      }
   }
}