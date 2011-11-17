using System;
using System.Collections;
using XF.Validation;

namespace XF.Validation
{
   public class NotEmptyAttribute : ValidationAttribute
   {
      public override string OptionalMessage { get; set; }

      protected override void Validate(object target, object rawValue, ValidationResult validationResult)
      {
         if (rawValue == null)
         {
            AddMessage(validationResult, "Value is null.");
            return;  
         }

         if (rawValue.GetType() == typeof (string))
         {
            var theValue = (string)rawValue;
            if (string.IsNullOrEmpty(theValue))
            {
               AddMessage(validationResult, "String is empty.");
               return;
            }
         }

         if (rawValue.GetType() == typeof (Guid))
         {
            var theValue = (Guid)rawValue;
            if (theValue == Guid.Empty)
            {
               AddMessage(validationResult, "Identifier is empty.");
               return;               
            }
         }

         if (rawValue is ICollection)
         {
            var theValue = rawValue as ICollection;
            if (theValue.Count == 0)
            {
               AddMessage(validationResult, "Collection is empty.");
               return;
            }
         }

         if (rawValue.GetType() == typeof(decimal))
         {
            var theValue = (decimal) rawValue;
            if (theValue == 0.0M)
            {
               AddMessage(validationResult,"Value is zero.");
               return;
            }
         }

         if (rawValue.GetType()== typeof(DateTime))
         {
            var theValue = (DateTime)rawValue;
            if (theValue == DateTime.MaxValue ||
                theValue == DateTime.MinValue) {
               AddMessage(validationResult, "Value is not set.");
               return;
            }
         }
      }
   }
}