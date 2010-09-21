using System;

namespace XF.Controls
{
   public interface IMaskedType
   {
      Type InputType { get; }
      string InputMask { get; }
      string GetFormattedLabel(object input);
      object ClearMask(string output);
      string CorrectedLength(string inputValue);
   }
}