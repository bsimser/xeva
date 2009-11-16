
namespace XF.Validation
{
   public class NumericAttribute : ValidationAttribute
   {
      private const string ERROR_MESSAGE = "Value must be numeric.";

      public override string OptionalMessage { get; set; }
      
      protected override void Validate(object target, object rawValue, ValidationResult validationResult)
      {
         if (rawValue == null) return;

         if (rawValue.GetType() == typeof(string))
            if (string.IsNullOrEmpty(rawValue.ToString())) return;

         decimal result;
         if (!decimal.TryParse(rawValue.ToString(), out result))
            AddMessage(validationResult, ERROR_MESSAGE);
      }
   }
}
