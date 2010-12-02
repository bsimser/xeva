using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace XF.UI.Smart
{
   public class ActionController<TService, TInputMessage, TUpdateMessage> : IActionCallbacks, IActionController
      where TService : IActionableService
   {
      private readonly TService _service;
      private readonly ActionPropertyParameters _actionPropertyRegistry = new ActionPropertyParameters();
      private readonly Dictionary<string, IControl> _controls = new Dictionary<string, IControl>();
      private TInputMessage _inputMessage;
      private MethodInfo _inputMethod;
      private MethodInfo _updateMethod;
      private ControllerValidator _validator;

      public ActionController(TService service, IActionView<TUpdateMessage> view)
      {
         _service = service;
         View = view;
         View.Attach(this);
      }

      public event EventHandler ActionComplete;
      public event EventHandler ActionCanceled;

      public IActionView<TUpdateMessage> View { get; private set; }
      public Guid EntityID { get; protected set; }
      public TUpdateMessage UpdateMessage { get; private set; }
      public IXFResults IXFResults { get; private set; }
      protected TService Service { get { return _service; } }

      public virtual void PerformAction()
      {
         UpdateMessage = View.RetrieveActionMessage();

         if (Equals(UpdateMessage, default(TUpdateMessage))) return;

         LoadEntityIDIntoUpdateMessage();

         LoadPassThroughPropertiesIntoUpdateMessage();

         if (!Validate(UpdateMessage)) return;

         IXFResults = _updateMethod.Invoke(Service, new object[] {UpdateMessage}) as IXFResults;

         switch (IXFResults.ResultCode) {
            case XFResultCode.Success:
         if (ActionComplete != null)
            ActionComplete(this, new EventArgs());
               break;
            case XFResultCode.Failure:
               View.ShowMessage(string.Format("Action failed: {0}", IXFResults.Message));
               if (ActionCanceled != null)
                  ActionCanceled(this, new EventArgs());
               break;
         }
      }

      private void LoadPassThroughPropertiesIntoUpdateMessage()
      {
         var properties = new List<PropertyInfo>(typeof(TUpdateMessage).GetProperties());
         _actionPropertyRegistry.ForEach(item =>
         {           
            if (item.IsPassthrough && properties.Exists(prop => prop.Name == item.Input.Name)) {
               var prop = properties.Find(p => p.Name == item.Input.Name);
               prop.SetValue(UpdateMessage, item.DefaultValue, null);
            }
         });
      }

      private void LoadEntityIDIntoUpdateMessage()
      {
         var entityProperty = UpdateMessage.GetType().GetProperty("EntityID");
         if(entityProperty != null)
            entityProperty.SetValue(UpdateMessage, EntityID, null);
      }

      public virtual void CancelAction()
      {
         if (ActionCanceled != null)
            ActionCanceled(this, new EventArgs());

         View.Close();
         Locator.Release(this);
      }

      public virtual void Finish()
      {
         View.Close();
         Locator.Release(this);
      }

      public void Activate()
      {
         Activate(new NullRequest());
      }

      public void Activate(IRequest request)
      {
         OnHandleRequest(request);

         if (_inputMethod != null)
            _inputMessage = (TInputMessage)_inputMethod.Invoke(_service, new object[] { EntityID });

         LoadActionPropertiesDefaultValues();

         foreach (var actionProperty in _actionPropertyRegistry)
         {
            if (actionProperty.IsPassthrough) continue;

            var controlValue = actionProperty.DefaultValue;
            EditableControl controlType;
            string propertyName, controlName, controlLabel;
            PopulateArgsFromOutputProperty(actionProperty.Output, out propertyName, out controlName, out controlLabel, out controlType);

            var lookupList = actionProperty.ListOfValues != null
                                ? actionProperty.ListOfValues.GetValue(_inputMessage, null) as List<IListMessage>
                                : null;
            var control = View.AddControl(propertyName, controlName, controlValue, controlLabel, controlType, lookupList);
            _controls.Add(propertyName, control);
         }

         View.Show();
      }

      private void LoadActionPropertiesDefaultValues()
      {
         foreach (var actionProperty in _actionPropertyRegistry)
         {
            if (actionProperty.DefaultValue != null) continue;

            if (actionProperty.Input == null)
               actionProperty.Input = _inputMessage.GetType().GetProperty(actionProperty.Output.Name, BindingFlags.Public | BindingFlags.Instance);

            if (actionProperty.Input != null)
               actionProperty.DefaultValue = actionProperty.Input.GetValue(_inputMessage, null);
         }
      }

      private void PopulateArgsFromOutputProperty(PropertyInfo outputInfo, out string propertyName, out string controlName,
                                                   out string controlLabel, out EditableControl controlType)
      {
         propertyName = outputInfo.Name;
         controlName = string.Format("_{0}Editor", outputInfo.Name);
         var label = string.Empty;
         var type = EditableControl.Unspecified;
         foreach (ActionPropertyAttribute attribute in outputInfo.GetCustomAttributes(typeof(ActionPropertyAttribute), false))
         {
            label = attribute.Label;
            type = attribute.EditorType;
         }

         controlLabel = !string.IsNullOrEmpty(label) ? label : outputInfo.Name;
         controlType = type;
      }

      protected virtual void OnHandleRequest(IRequest request) { }

      protected bool Validate(object target)
      {
         if (_validator == null)
            InitializeValidator(new ControllerValidator());
         return _validator.Validate(new[] { target }, _controls);
      }

      private void InitializeValidator(ControllerValidator validator)
      {
         _validator = validator;
      }

      #region Mapping Code

      public ActionController<TService, TInputMessage, TUpdateMessage> Titled(string title)
      {
         View.Title = title;
         return this;
      }

      public ActionController<TService, TInputMessage, TUpdateMessage> ForEntity(Guid entityID)
      {
         EntityID = entityID;
         return this;
      }

      public ActionController<TService, TInputMessage, TUpdateMessage> Input<TArg>(Func<TArg, TInputMessage> inputMethod, TArg inputArg)
      {
         _inputMethod = inputMethod.Method;
         return this;
      }

      public ActionController<TService, TInputMessage, TUpdateMessage> Update(Func<TUpdateMessage, IXFResults> updateMethod)
      {
         _updateMethod = updateMethod.Method;
         return this;
      }

      public ActionController<TService, TInputMessage, TUpdateMessage> Map(Expression<Func<TUpdateMessage, object>> updateField, object value)
      {
         var updateProperty = ExpressionsHelper.GetMemberInfo(updateField) as PropertyInfo;

         if (updateProperty == null) return this;

         _actionPropertyRegistry.Add(new ActionPropertyParameters { Output = updateProperty, DefaultValue = value });
         return this;
      }

      public ActionController<TService, TInputMessage, TUpdateMessage> Map(Expression<Func<TUpdateMessage, object>> updateField,
                                                                           Expression<Func<TInputMessage, object>> inputField)
      {
         var updateProperty = ExpressionsHelper.GetMemberInfo(updateField) as PropertyInfo;
         var inputProperty = ExpressionsHelper.GetMemberInfo(inputField) as PropertyInfo;
         if (updateProperty == null) return this;
         if (inputProperty == null) return this;

         _actionPropertyRegistry.Add(new ActionPropertyParameters { Output = updateProperty, Input = inputProperty });
         return this;
      }

      public ActionController<TService, TInputMessage, TUpdateMessage> MapPassthrough(Expression<Func<TUpdateMessage, object>> updateField,
                                                                           Expression<Func<TInputMessage, object>> inputField) {
         var updateProperty = ExpressionsHelper.GetMemberInfo(updateField) as PropertyInfo;
         var inputProperty = ExpressionsHelper.GetMemberInfo(inputField) as PropertyInfo;
         if (updateProperty == null) return this;
         if (inputProperty == null) return this;

         _actionPropertyRegistry.Add(new ActionPropertyParameters { Output = updateProperty, Input = inputProperty, IsPassthrough = true });
         return this;
      }



      public ActionController<TService, TInputMessage, TUpdateMessage> Map(Expression<Func<TUpdateMessage, object>> updateField,
                                                                           Expression<Func<TInputMessage, object>> inputField,
                                                                           Expression<Func<TInputMessage, object>> listOfValues)
      {
         var updateProperty = ExpressionsHelper.GetMemberInfo(updateField) as PropertyInfo;
         var inputProperty = ExpressionsHelper.GetMemberInfo(inputField) as PropertyInfo;
         var listProperty = ExpressionsHelper.GetMemberInfo(listOfValues) as PropertyInfo;
         if (updateProperty == null) return this;
         if (inputProperty == null) return this;
         if (listProperty == null) return this;

         _actionPropertyRegistry.Add(new ActionPropertyParameters { Output = updateProperty, Input = inputProperty, ListOfValues = listProperty });
         return this;
      }

      public ActionController<TService, TInputMessage, TUpdateMessage> Map(Expression<Func<TUpdateMessage, object>> updateField,
                                                                           object value,
                                                                           Expression<Func<TInputMessage, object>> listOfValues)
      {
         var updateProperty = ExpressionsHelper.GetMemberInfo(updateField) as PropertyInfo;
         var listProperty = ExpressionsHelper.GetMemberInfo(listOfValues) as PropertyInfo;
         if (updateProperty == null) return this;
         if (listProperty == null) return this;

         _actionPropertyRegistry.Add(new ActionPropertyParameters { Output = updateProperty, DefaultValue = value, ListOfValues = listProperty });
         return this;
      }

      public ActionController<TService, TInputMessage, TUpdateMessage> Map(Expression<Func<TUpdateMessage, object>> updateField)
      {
         var updateProperty = ExpressionsHelper.GetMemberInfo(updateField) as PropertyInfo;
         if (updateProperty == null) return this;

         _actionPropertyRegistry.Add(new ActionPropertyParameters { Output = updateProperty });
         return this;
      }

      #endregion Mapping Code

   }
}