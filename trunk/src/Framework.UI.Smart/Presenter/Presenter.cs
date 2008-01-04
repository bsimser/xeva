using System.Collections.Generic;
using System.Reflection;
using XEVA.Framework.UI.Smart;
using XEVA.Framework.Validation;

namespace XEVA.Framework.UI.Smart
{
   public abstract class Presenter<TView> : IPresenter
      where TView : IView
   {
      private TView _view;
      private bool _isStarted = false;
      private bool _isFinished = false;
      private string _key;
      private string _label;
      private object _parentForm;
      private IContext _context;
      private IFormBuilder _formBuilder;
      private List<IPresenter> _presenters = new List<IPresenter>();
      private readonly Dictionary<string, IValidationObject> _validationObjects
         = new Dictionary<string, IValidationObject>();

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

      public object ParentForm
      {
         get { return _parentForm; }
         set { _parentForm = value; }
      }

      public IContext Context
      {
         get { return _context; }
         set { _context = value; }
      }

      public List<string> ContextList
      {
         get
         {
            List<string> result = new List<string>();
            foreach (KeyValuePair<string, object> statePair in Context.State)
            {
               result.Add(statePair.Key);
            }

            return result;
         }
      }

      public void Start()
      {
         if (_context == null)
            RegisterPresenterContext(this);

         if (_isStarted) return;
         CustomStart();

         _isStarted = true;
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

      public Dictionary<string, IValidationObject> ValidatoinObjects
      {
         get { return _validationObjects; }
      }

      public IFormBuilder FormBuilder
      {
         get
         {
            if (_formBuilder == null)
               _formBuilder = IoC.Resolve<IFormBuilder>();

            return _formBuilder;
         }
         set { _formBuilder = value; }
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

      public void AddContext(string contextKey, object contextValue)
      {
         _context[contextKey] = contextValue;
      }

      public void RemoveContext(string contextKey)
      {
         _context.Remove(contextKey);
      }

      public object GetContext(string contextKey)
      {
         object state = Context[contextKey];
         if (state is PresenterState)
            return ((PresenterState)state).PropertyValue;
         else
            return state;
      }

      public TPresenter CreateChildPresenter<TPresenter>()
      {
         TPresenter result = IoC.Resolve<TPresenter>();
         RegisterPresenterContext((IPresenter)result);
         LoadPresenterContext((IPresenter)result);
         _presenters.Add((IPresenter)result);

         return result;
      }

      public IPresenter CreateChildPresenter(string containerKey)
      {
         IPresenter result = (IPresenter)IoC.Resolve(containerKey);
         RegisterPresenterContext(result);
         LoadPresenterContext(result);
         _presenters.Add(result);

         return result;
      }

      public PType CreateDisconnectedPresenterWithoutContext<PType>()
      {
         PType result = IoC.Resolve<PType>();

         return result;
      }

      public PType CreateDisconnectedPresenter<PType>()
      {
         PType result = IoC.Resolve<PType>();
         RegisterPresenterContext((IPresenter)result);
         LoadPresenterContext((IPresenter)result);

         return result;
      }

      public PType CreateDisconnectedPresenter<PType>(string viewName, string layoutType)
      {
         PType result = IoC.Resolve<PType>();
         RegisterPresenterContext((IPresenter)result);
         LoadPresenterContext((IPresenter)result);

         _formBuilder.CreateLayout(viewName, (IPresenter)result, layoutType);

         return result;
      }

      protected void LaunchPresenterInLayout(IPresenter presenter, string viewName, string layoutType)
      {
         _formBuilder.CreateLayout(viewName, presenter, layoutType);
      }

      public void AddValidationObject(string property, IValidationObject validationObject)
      {
         if (!_validationObjects.ContainsKey(property))
            _validationObjects.Add(property, validationObject);
      }

      private void RegisterPresenterContext(IPresenter presenter)
      {
         if (_context == null)
            _context = new Context();
         presenter.Context = _context;

         List<PropertyInfo> presenterProps = new List<PropertyInfo>
            (presenter.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic));

         foreach (PropertyInfo property in presenterProps)
         {
            if (property.GetCustomAttributes(typeof(ParameterAttribute), true).Length > 0)
            {
               if (property.CanRead &&
                   !presenter.Context.Contains(property.Name))
               {
                  PresenterState state = new PresenterState(property, presenter);
                  presenter.Context[property.Name] = state;
               }
            }
         }
      }

      private void LoadPresenterContext(IPresenter presenter)
      {
         foreach (KeyValuePair<string, object> state in Context.State)
         {
            PropertyInfo property = presenter.GetType().GetProperty(state.Key);
            if (property != null)
               if (property.CanWrite)
                  property.SetValue(presenter, GetContext(state.Key), null);
         }
      }
   }
}
