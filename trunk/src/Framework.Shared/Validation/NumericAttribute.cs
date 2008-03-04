
namespace XF.Validation
{
   public class NumericAttribute : ValidationAttribute
   {
      private const string ERROR_MESSAGE = "Value must be numeric.";

      protected override void Validate(object target, object rawValue, ValidationResult validationResult)
      {
         if (rawValue == null) return;

         decimal result;
         if (!decimal.TryParse(rawValue.ToString(), out result))
            AddMessage(validationResult, ERROR_MESSAGE);
      }
   }
}
