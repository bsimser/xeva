using System;
using System.Collections.Generic;
using System.Reflection;

namespace XF.Validation
{
   public class Validator : IValidator
   {
      private IDictionary<Type, IList<ValidationAttribute>> _attributeCache =
         new Dictionary<Type, IList<ValidationAttribute>>();

      public Validator() {}

      public ValidationResult Validate(object[] targets, Dictionary<string, IValidationAware> validationObjects)
      {
         var result = new ValidationResult();

         foreach (object target in targets)
         {
            Type targetType = target.GetType();
            IList<ValidationAttribute> validations = ScanTypeForValidationAttributes(targetType);

            foreach (ValidationAttribute attribute in validations)
               attribute.Validate(target, result);

            if (target is ISelfValidator)
            {
               ((ISelfValidator)target).Validate(result);
            }
         }

         DisplayErrorNotifications(result, validationObjects);

         return result;
      }

      private IList<ValidationAttribute> ScanTypeForValidationAttributes(Type type)
      {
         if (_attributeCache.ContainsKey(type))
            return _attributeCache[type];

         var result = new List<ValidationAttribute>();

         PropertyInfo[] properties = type.GetProperties();

         for (int c1 = 0; c1 < properties.Length; c1++)
         {
            object[] attributes = properties[c1].GetCustomAttributes(typeof (ValidationAttribute), true);

            for (int c2 = 0; c2 < attributes.Length; c2++)
            {
               var attribute = (ValidationAttribute)attributes[c2];
               // Set the attribute's "Property"
               attribute.Property = properties[c1];
               result.Add(attribute);
            }
         }

         _attributeCache.Add(type, result);

         return result;
      }

      private void DisplayErrorNotifications(ValidationResult validationResult, Dictionary<string, IValidationAware> validationObjects)
      {
         var messages = new List<ValidatonError>(validationResult.Errors);

         foreach (KeyValuePair<string, IValidationAware> validationObject in validationObjects)
         {
            ValidatonError message = messages.Find(match => match.Property == validationObject.Key);

            if (message != null)
               validationObject.Value.ShowError(message.Message);
            else 
               validationObject.Value.ClearError();
         }
      }
   }
}