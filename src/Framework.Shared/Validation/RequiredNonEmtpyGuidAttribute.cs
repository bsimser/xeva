using System;

namespace XEVA.Framework.Validation
{
   public class RequiredNonEmptyGuidAttribute : ValidationAttribute
   {
      private const string ERROR_MESSAGE = "Field is required";

      protected override void Validate(object target, object rawValue, Notification notification)
      {
         Guid guidValue = new Guid(rawValue.ToString());

         if (guidValue == Guid.Empty)
         {
            AddMessage(notification, ERROR_MESSAGE);
         }
      }
   }
}
