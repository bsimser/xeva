using System;
using System.Collections;
using XF.Validation;

namespace XF.Validation
{
   public class NotEmptyAttribute : ValidationAttribute
   {
      protected override void Validate(object target, object rawValue, ValidationResult validationResult)
      {
         if (rawValue == null)
         {
            AddMessage(validationResult, "Value is null.");
            return;  
         }

         if (rawValue.GetType() == typeof (string))
         {
            string theValue = (string)rawValue;
            if (string.IsNullOrEmpty(theValue))
            {
               AddMessage(validationResult, "String is empty.");
               return;
            }
         }

         if (rawValue.GetType() == typeof (Guid))
         {
            Guid theValue = (Guid)rawValue;
            if (theValue == Guid.Empty)
            {
               AddMessage(validationResult, "Identifier is empty.");
               return;               
            }
         }

         if (rawValue is ICollection)
         {
            ICollection theValue = rawValue as ICollection;
            if (theValue.Count == 0)
            {
               AddMessage(validationResult, "Collection is empty.");
               return;
            }
         }
      }
   }
}