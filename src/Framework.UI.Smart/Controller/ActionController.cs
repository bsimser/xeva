using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace XF.UI.Smart
{
   public class ActionController<TService, TInputMessage, TUpdateMessage> : IActionCallbacks 
      where TService : IActionableService 
   {
      private readonly TService _service;
      private readonly ActionPropertyParameters _actionPropertyRegistry = new ActionPropertyParameters();
      private Func<TService, TInputMessage> _inputMethod;
      private Func<TService, string> _updateMethod;
      private TInputMessage _inputMessage;

      public ActionController(TService service, IActionView<TUpdateMessage> view)
      {
         _service = service;
         View = view;
         View.Attach(this);
      }

      public event EventHandler ActionComplete;
      public event EventHandler ActionCanceled;

      public IActionView<TUpdateMessage> View { get; private set; }
      public Guid EntityID { get; private set; }
      public TUpdateMessage UpdateMessage { get; private set; }

      public virtual void PerformAction()
      {
         UpdateMessage = View.RetrieveActionMessage();

         if (ActionComplete != null)
            ActionComplete(this, new EventArgs());
      }

      public void Activate()
      {
         if (_inputMethod != null)
            _inputMessage = _inputMethod.Invoke(_service);

         LoadActionPropertiesDefaultValues();

         foreach (var actionProperty in _actionPropertyRegistry)
         {
            var lookupList = actionProperty.ListOfValues != null
                                ? actionProperty.ListOfValues.GetValue(_inputMessage, null) as List<IListMessage>
                                : null;
            View.AddControl(actionProperty.Output, actionProperty.DefaultValue, lookupList);
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

      #region Mapping Code

      public ActionController<TService, TInputMessage, TUpdateMessage> ForEntity(Guid entityID)
      {
         EntityID = entityID;
         return this;
      }

      public ActionController<TService, TInputMessage, TUpdateMessage> Input(Func<TService, TInputMessage> inputMethod)
      {
         _inputMethod = inputMethod;
         return this;
      }

      public ActionController<TService, TInputMessage, TUpdateMessage> Update(Func<TService, string> updateMethod)
      {
         _updateMethod = updateMethod;
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