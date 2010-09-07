using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using XF;
using XF.UI.Smart;

namespace XF.UI.Smart {
   public class FieldsRegistry<TMessage> : IFieldsRegistry<TMessage> {
      private readonly TMessage _message;
      private readonly List<KeyValuePair<string, IEditable>> _registry = new List<KeyValuePair<string, IEditable>>();
      private readonly List<KeyValuePair<string, object>> _valuesRegistry = new List<KeyValuePair<string, object>>();
      private readonly IViewCallbacks _presenter;
      private readonly List<IEditable> _registeredControls = new List<IEditable>();
      private readonly List<IEditable> _editControls = new List<IEditable>();
      private IHaveMessageToSave _view;

      public FieldsRegistry(IViewCallbacks presenter, IHaveMessageToSave view) {
         _message = Activator.CreateInstance<TMessage>();
         _presenter = presenter;
         _view = view;
      }

      public List<IEditable> RegisteredControls {
         get { return _registeredControls; }
      }

      public List<IEditable> EditControls {
         get { return _editControls; }
      }

      public FieldsRegistry<TMessage> Map(Expression<Func<TMessage, object>> expression, IEditable control) {
         var memberInfo = ExpressionsHelper.GetMemberInfo(expression);

         if (memberInfo == null) return this;

         var property = memberInfo.Name;
         _registry.Add(new KeyValuePair<string, IEditable>(property, control));

         _presenter.RegisterControl<TMessage>(expression, control as IControl);

         if (!_registeredControls.Contains(control))
            _registeredControls.Add(control);
         return this;
      }

      public FieldsRegistry<TMessage> Map(Expression<Func<TMessage, object>> expression, object value) {
         var memberInfo = ExpressionsHelper.GetMemberInfo(expression);

         if (memberInfo == null) return this;

         var property = memberInfo.Name;
         _valuesRegistry.Add(new KeyValuePair<string, object>(property, value));
         return this;
      }

      public FieldsRegistry<TMessage> Edit(IEditable control) {
         if (!_editControls.Contains(control))
            _editControls.Add(control);
         return this;
      }

      //public List<IEditable> RegisteredControls() {
      //   var registeredControls = new List<IEditable>(_registeredControls);
      //   return registeredControls;
      //}

      public TMessage GetHydratedMessage() {
         var properties = new List<PropertyInfo>(typeof(TMessage).GetProperties());
         _registry.ForEach(item => {
                        if (properties.Exists(prop => prop.Name == item.Key)) {
                           var prop = properties.Find(p => p.Name == item.Key);
                           prop.SetValue(_message, item.Value.EditedValue, null);
                        }
                     });

         _valuesRegistry.ForEach(value => {
                                       if (properties.Exists(prop => prop.Name == value.Key)) {
                                          var property = properties.Find(p => p.Name == value.Key);
                                          property.SetValue(_message, value.Value, null);
                                       }
                                    });
         return _message;
      }

      public bool Validate() {
         var message = GetHydratedMessage();
         _view.MessageToSave = message;

         return _presenter.Validate(message);
      }
   }
}
