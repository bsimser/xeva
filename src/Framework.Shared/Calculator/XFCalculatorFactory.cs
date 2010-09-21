using System;
using System.IO;
using System.Xml.Serialization;

namespace Model {
   public sealed class XFCalculatorFactory {
      public static XFCalculator BuildCalculator(string calcPath, XFCalculatorVariable input) {
         var serializer = new XmlSerializer(typeof(XFCalculator));
         try {
            var calculator = serializer.Deserialize(File.OpenRead(calcPath)) as XFCalculator;
            calculator.Initialize(input, new XFCalculatorToolKit());

            return calculator;
         }
         catch (Exception ex) {
            throw new XFCalculatorException("Unable to build calculator", ex);
         }         
      }
   }
}