
namespace XF.Validation
{
   public class NotNegativeAttribute : ValidationAttribute
   {
      private const string ERROR_MESSAGE = "Value cannot be negative.";

      protected override void Validate(object target, object rawValue, ValidationResult validationResult)
      {
         if (rawValue == null) return;

         decimal result;
         if (!decimal.TryParse(rawValue.ToString(), out result)) return;

         if (result < 0)
            AddMessage(validationResult, ERROR_MESSAGE);
      }
   }
}
