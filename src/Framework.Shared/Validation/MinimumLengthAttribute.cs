using System;

namespace XEVA.Framework.Validation
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

      protected override void Validate(object target, object rawValue, Notification notification)
      {
         if (!(rawValue is string)) return;

         if (rawValue.ToString().Length < _minimumLength)
            AddMessage(notification, string.Format("Must be {0} or more characters.", _minimumLength));
      }
   }
}