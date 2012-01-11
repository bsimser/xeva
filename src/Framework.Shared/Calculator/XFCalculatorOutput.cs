using System;
using System.Xml.Serialization;

namespace XF {
   public class XFCalculatorOutput {
      private object _value;

      [XmlAttribute(AttributeName = "name", DataType = "string")]
      public string Name { get; set; }

      [XmlAttribute(AttributeName = "rounding", DataType = "string")]
      public string Rounding { get; set; }

      public object Value {
         get { return _value; }
         set {
            _value = value == null 
                     || value.GetType() != typeof(decimal)
                     || string.IsNullOrEmpty(Rounding)
                  ? value
                  : RoundOutput(Rounding, (decimal)value);
         }
      }

      public object Reference { get; set; }

      private static object RoundOutput(string rounding, decimal operand) {
         var method = (XFCalculatorRound)Enum.Parse(typeof(XFCalculatorRound), rounding);
         switch (method) {
            case XFCalculatorRound.ToEven:
               return Math.Round(operand, 2, MidpointRounding.ToEven);
            case XFCalculatorRound.AwayFromZero:
               return Math.Round(operand, 2, MidpointRounding.AwayFromZero);
            case XFCalculatorRound.ToWhole:
               return Math.Round(operand, 0, MidpointRounding.ToEven);
            case XFCalculatorRound.TruncateToWhole:
               return decimal.Truncate(operand);
            case XFCalculatorRound.None:
               return operand;
         }
         return operand;
      }
   }
}