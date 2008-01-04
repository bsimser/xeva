using System.Text.RegularExpressions;

namespace XEVA.Framework.Validation
{
   public class RequiredNonSpecialCharacterAttribute : ValidationAttribute
   {
      private const string ERROR_MESSAGE = "Not a valid character string.";

      private readonly string pattern = @"^[0-9a-zA-Z\._-]*$";

      protected override void Validate(object target, object rawValue, Notification notification)
      {
         string stringValue = rawValue as string;

         if (string.IsNullOrEmpty(stringValue))
         {
            AddMessage(notification, ERROR_MESSAGE);
            return;
         }

         Regex charRegex = new Regex(pattern);

         if (!charRegex.IsMatch(stringValue))
            AddMessage(notification, ERROR_MESSAGE);
      }
   }
}