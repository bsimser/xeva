using System;
using System.Reflection;

namespace XF.Validation
{
   [AttributeUsage(AttributeTargets.Property)]
   public abstract class ValidationAttribute : Attribute
   {
      private PropertyInfo _property;

      public void Validate(object target, ValidationResult validationResult)
      {
         object rawValue = _property.GetValue(target, null);
         Validate(target, rawValue, validationResult);
      }

      protected void AddMessage(ValidationResult validationResult, string message)
      {
         validationResult.AddError(Property.Name, !string.IsNullOrEmpty(OptionalMessage) ? OptionalMessage : message);
      }

      protected abstract void Validate(object target, object rawValue, ValidationResult validationResult);

      public PropertyInfo Property
      {
         get { return _property; }
         set { _property = value; }
      }

      public string PropertyName
      {
         get { return _property.Name; }
      }

      public abstract string OptionalMessage { get; set; }
   }
}