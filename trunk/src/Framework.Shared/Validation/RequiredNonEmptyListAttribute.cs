using System.Collections;

namespace XEVA.Framework.Validation
{
   public class RequiredNonEmptyListAttribute : ValidationAttribute
   {
      private const string ERROR_MESSAGE = "At Least 1 Value is Required";

      protected override void Validate(object target, object rawValue, Notification notification)
      {
         IList listValue = (IList)rawValue;

         if (listValue == null || listValue.Count == 0)
         {
            AddMessage(notification, ERROR_MESSAGE);
         }
      }
   }
}
