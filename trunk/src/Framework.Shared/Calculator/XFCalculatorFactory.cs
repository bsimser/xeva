using System;
using System.IO;
using System.Xml.Serialization;

namespace XF {
   public sealed class XFCalculatorFactory {
      public static XFCalculator BuildCalculator(string calcPath) {
         var serializer = new XmlSerializer(typeof(XFCalculator));
         try {
            using (var fs = File.OpenRead(calcPath)) {
               var calculator = serializer.Deserialize(fs) as XFCalculator;
               calculator.InitializeToolKit(new XFCalculatorToolKit());

               return calculator;
            }
         }
         catch (Exception ex) {
            throw new XFCalculatorException("Unable to build calculator", ex);
         }
      }

      public static XFCalculator BuildCalculator(string calcPath, XFCalculatorVariable input) {
         var serializer = new XmlSerializer(typeof(XFCalculator));
         try {
            using (var fs = File.OpenRead(calcPath)) {
               var calculator = serializer.Deserialize(fs) as XFCalculator;
               calculator.InitializeToolKit(new XFCalculatorToolKit());
               calculator.InitializeInput(input);

               return calculator;
            }
         }
         catch (Exception ex) {
            throw new XFCalculatorException("Unable to build calculator", ex);
         }
      }

      public static XFCalculator BuildCalculator(StringReader reader, XFCalculatorVariable input) {
         var serializer = new XmlSerializer(typeof(XFCalculator));
         try {
            var calculator = serializer.Deserialize(reader) as XFCalculator;
            calculator.InitializeToolKit(new XFCalculatorToolKit());
            calculator.InitializeInput(input);

            return calculator;
         }
         catch (Exception ex) {
            throw new XFCalculatorException("Unable to build calculator", ex);
         }
      }

   }
}