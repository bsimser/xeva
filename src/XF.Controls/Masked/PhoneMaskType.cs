using System;
using System.Text.RegularExpressions;

namespace XF.Controls {
   public class PhoneMaskType : IMaskedType {
      public Type InputType {
         get {
            return typeof(string);
         }
      }

      public string InputMask {
         get { return "(###) ###-####"; }
      }

      public string GetFormattedLabel(object input) {
         var mask = GetLabelMask(input.ToString().Length);
         var convertedInput = Convert.ToInt64(input);
         return string.Format("{0:" + mask + "}", convertedInput).Replace("()", "");
      }

      private string GetLabelMask(int length) {
         switch (length) {
            case 7:
               return "###-####";
            case 10:
               return "(###) ###-####";
            case 11:
               return "#(###) ###-####";
            case 12:
               return "#(###) ###-#### #";
            case 13:
               return "#(###) ###-#### ##";
            case 14:
               return "#(###) ###-#### ###";
            case 15:
               return "#(###) ###-#### ####";
            default:
               return "###-####";
         }
      }

      public object ClearMask(string output) {
         var regex = new Regex("[^0-9]");
         var result = regex.Replace(output, "");
         return result;
      }

      public string CorrectedLength(string inputValue)
      {
         if (inputValue.Length == 7) return string.Format("    {0}", inputValue);
         return inputValue;
      }
   }
}