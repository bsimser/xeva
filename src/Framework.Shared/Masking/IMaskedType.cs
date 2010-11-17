using System;

namespace XF
{
   public interface IMaskedType
   {
      Type InputType { get; }
      string InputMask { get; }
      string GetFormattedValue(object input);
      object ClearMask(string output);
      string CorrectedLength(string inputValue);
   }
}