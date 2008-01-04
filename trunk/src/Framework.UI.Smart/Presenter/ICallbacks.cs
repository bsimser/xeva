using XEVA.Framework.Validation;

namespace XEVA.Framework.UI.Smart
{
   public interface ICallbacks
   {
      void AddValidationObject(string property, IValidationObject validationObject);
   }
}
