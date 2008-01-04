using System;

namespace XEVA.Framework.Validation
{
   [AttributeUsage(AttributeTargets.Property)]
   public class RequiredAttribute : ValidationAttribute
   {
      protected override void Validate(object target, object rawValue, Notification notification)
      {
         if (rawValue == null)
            AddMessage(notification, "Field is required.");
      }
   }
}