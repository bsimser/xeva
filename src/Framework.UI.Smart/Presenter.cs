using System;
using System.Collections.Generic;
using XF.UI.Smart;

namespace XF.UI.Smart
{
   public abstract class Presenter<TView, TCallbacks> : IPresenter
      where TCallbacks : class, IViewCallbacks
      where TView : class, IView<TCallbacks>
   {
      private TView _view;
      private IPresenterValidator _presenterValidator;
      private bool _isStarted = false;
      private bool _isFinished = false;
      private string _key;
      private string _label;
      private readonly Dictionary<string, IControl> _controls = new Dictionary<string, IControl>();
      private IRequest _request;
      private IWindowAdapter _windowAdapter;
      private IWindowRegistry _windowRegistry;

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

      protected bool HasStarted
      {
         get { return _isStarted; }
      }

      protected bool HasFinished
      {
         get { return _isFinished; }
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

         TCallbacks callbacks = this as TCallbacks;
         if (callbacks == null) throw new NoCallbacksImplementationException();
         
         View.Attach(callbacks);
         CustomStart();

         Window.Show();

         _isStarted = true;
      }

      protected virtual void InitializeRequest(IRequest request) { }

      public virtual void InitializeValidator(IPresenterValidator presenterValidator)
      {
         if (_presenterValidator == null)
            _presenterValidator = presenterValidator;
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
      }


      public virtual object UI
      {
         get
         {
            object result = (_view == null) ? null : _view.UI;
            return result;
         }
      }

      protected virtual void CustomStart()
      {
      }

      protected virtual void CustomFinish()
      {
         if (_windowRegistry == null) return;
         Guid entityID = _request.GetOptionalItem<Guid>("entity-id", Guid.Empty);
         _windowRegistry.RemoveWindow(entityID);
      }

      public void DisplayIn(IWindowAdapter windowAdapter)
      {
         if (this.UI == null) throw new NoUserInterfaceObjectException();
         _windowAdapter = windowAdapter;
         _windowAdapter.InitializeUI(this.UI);
         _windowAdapter.Closed += OnWindowClosed;
         if (HasStarted) _windowAdapter.Show();
      }

      public IWindowController Window
      {
         get
         {
            return (IWindowController)_windowAdapter ?? new NoWindowControls();
         }
      }

      public IWindowRegistry WindowRegistry
      {
         set { _windowRegistry = value; }
      }

      private void OnWindowClosed(object sender, EventArgs e)
      {
         Finish(true);
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

      public void RegisterControl(string property, IControl control)
      {
         _controls.Add(property, control);
      }

      public bool Validate(object target)
      {
         return Validate(new object[1] { target });
      }

      public bool Validate(object[] targets)
      {
         InitializeValidator(new PresenterValidator());

         return _presenterValidator.Validate(targets, _controls);
      }
   }
}
