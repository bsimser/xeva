using System;
using System.Collections.Generic;
using XEVA.Framework.UI.Smart;

namespace XEVA.Framework.UI.Smart
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
      private IWindow _window;

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

      public void Start()
      {
         Start(new NullRequest());
      }

      public void Start(IRequest request)
      {
         if (_isStarted) return;
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
         if (!_isStarted) return;
         if (_isFinished) return;
         if (!windowInitiated && (_window != null))
         {
            _window.Closed -= OnWindowClosed;
            _window.Close();
         }
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
      }

      public void DisplayIn(IWindow window)
      {
         if (this.UI == null) throw new NoUserInterfaceObjectException();
         _window = window;
         _window.InitializeUI(this.UI);
         window.Closed += OnWindowClosed;
      }

      public IWindowController Window
      {
         get
         {
            return (IWindowController)_window ?? new NoWindowControls();
         }
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
         InitializeValidator(new PresenterValidator());

         return _presenterValidator.Validate(target, _controls);
      }
   }
}
