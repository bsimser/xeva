using System.Text.RegularExpressions;

namespace XF.Validation
{
   public class MatchPatternAttribute : ValidationAttribute
   {
      private const string ERROR_MESSAGE = "Not a valid character string.";

      private string _pattern;

      public MatchPatternAttribute(string pattern)
      {
         _pattern = pattern;
      }

      protected override void Validate(object target, object rawValue, ValidationResult validationResult)
      {
         string stringValue = rawValue as string;

         Regex charRegex = new Regex(_pattern);

         if (!charRegex.IsMatch(stringValue))
            AddMessage(validationResult, ERROR_MESSAGE);
      }
   }
}