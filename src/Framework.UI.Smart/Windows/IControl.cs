using XF.Validation;

namespace XF.UI.Smart
{
   public interface IControl : IValidationAware
   {
      string Label { get; set; }

      object Value { get; set; }

      bool ReadOnly { get; set; }

      int Width { get; set; }

      int Height { get; set; }
   }
}