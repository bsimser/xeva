using System;
using System.Reflection;

namespace XEVA.Framework.Validation
{
   [AttributeUsage(AttributeTargets.Property)]
   public abstract class ValidationAttribute : Attribute
   {
      private PropertyInfo _property;

      public void Validate(object target, Notification notification)
      {
         object rawValue = _property.GetValue(target, null);
         Validate(target, rawValue, notification);
      }

      protected void AddMessage(Notification notification, string message)
      {
         notification.AddMessage(Property.Name, message);
      }

      protected abstract void Validate(object target, object rawValue, Notification notification);

      public PropertyInfo Property
      {
         get { return _property; }
         set { _property = value; }
      }

      public string PropertyName
      {
         get { return _property.Name; }
      }
   }
}