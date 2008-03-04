using System.Text.RegularExpressions;

namespace XF.Validation
{
   public class EmailFormatAttribute : ValidationAttribute
   {
      private const string ERROR_MESSAGE = "Not a valid email address.";

      private string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
      + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
      + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
      + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
      + @"[a-zA-Z]{2,}))$";

      protected override void Validate(object target, object rawValue, ValidationResult validationResult)
      {

         string stringValue = rawValue as string;

         if (stringValue == null)
         {
            AddMessage(validationResult, ERROR_MESSAGE);
            return;
         }

         Regex emailRegex = new Regex(patternStrict);

         if (!emailRegex.IsMatch(stringValue))
            AddMessage(validationResult, ERROR_MESSAGE);
      }
   }
}
