using System;

namespace XF.Validation
{
   [AttributeUsage(AttributeTargets.Property)]
   public class RequiredAttribute : ValidationAttribute
   {
      protected override void Validate(object target, object rawValue, ValidationResult validationResult)
      {
         if (rawValue == null)
            AddMessage(validationResult, "Field is required.");
      }
   }
}