using System;

namespace XEVA.Framework.Validation
{
   public class RequiredNonEmptyStringAttribute : ValidationAttribute
   {
      private const string ERROR_MESSAGE = "Field is required";

      protected override void Validate(object target, object rawValue, Notification notification)
      {
         string stringValue = rawValue as string;

         if (String.IsNullOrEmpty(stringValue))
         {
            AddMessage(notification, ERROR_MESSAGE);
         }
      }
   }
}
