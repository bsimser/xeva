using XF.Validation;

namespace XF.Validation
{
   public interface ISelfValidator
   {
      void Validate(ValidationResult validationResults);
   }
}