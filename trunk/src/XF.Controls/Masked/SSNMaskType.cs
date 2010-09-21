using System;
using System.Text.RegularExpressions;

namespace XF.Controls
{
   public class SSNMaskType :IMaskedType
   {
      public Type InputType 
      { 
         get {
            return typeof (string);
         }
      }
      
      public string InputMask 
      { 
         get { return "##-###-####"; }
      }

      public string GetFormattedLabel(object input)
      {
         var convertedValue = Convert.ToInt64(input);

         return string.Format("{0:" + InputMask + "}",convertedValue);
      }

      public object ClearMask(string output)
      {
         var regex = new Regex("[^0-9]");
         var result = regex.Replace(output, "");
         return result;
      }

      public string CorrectedLength(string inputValue) {
         return inputValue;
      }
   }
}