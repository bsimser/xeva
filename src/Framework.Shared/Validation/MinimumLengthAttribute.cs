using System;

namespace XF.Validation
{
   public class MinimumLengthAttribute : ValidationAttribute
   {
      private int _minimumLength;

      public MinimumLengthAttribute(int minimumLength)
      {
         if (minimumLength <= 1) 
            throw new ArgumentOutOfRangeException("minimumLength", minimumLength, "Must be >= 1");

         _minimumLength = minimumLength;
      }

      public override string OptionalMessage { get; set; }

      protected override void Validate(object target, object rawValue, ValidationResult validationResult)
      {
         if (!(rawValue is string)) return;

         if (rawValue.ToString().Length < _minimumLength)
            AddMessage(validationResult, string.Format("Must be {0} or more characters.", _minimumLength));
      }
   }
}