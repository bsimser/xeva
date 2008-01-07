using System.Collections.Generic;
using System.Reflection;
using Specs_for_Presenter;
using XEVA.Framework.UI.Smart;
using XEVA.Framework.Validation;

namespace XEVA.Framework.UI.Smart
{
   public abstract class Presenter<TView, TCallbacks> : IPresenter
      where TCallbacks : class, IPresenterCallbacks
      where TView : class, IView<TCallbacks>
   {
      private TView _view;
      private bool _isStarted = false;
      private bool _isFinished = false;
      private string _key;
      private string _label;
      private readonly Dictionary<string, IControl> _controls = new Dictionary<string, IControl>();

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

      public void Start(IRequest request)
      {
         if (_isStarted) return;

         if (View == null) throw new ViewNotAvailableException();

         if (!ValidateRequest(request)) throw new InvalidRequestException();

         TCallbacks callbacks = this as TCallbacks;
         if (callbacks == null) throw new NoCallbacksImplementationException();
         View.Attach(callbacks);
         
         CustomStart();

         _isStarted = true;
      }

      protected virtual bool ValidateRequest(IRequest request)
      {
         return true;
      }

      public void Finish()
      {
         if (!_isStarted) return;
         if (_isFinished) return;
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

      public virtual void CustomStart()
      {
      }

      public virtual void CustomFinish()
      {
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
   }
}
