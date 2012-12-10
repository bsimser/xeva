using System;
using System.IO;
using System.Xml.Serialization;

namespace XF {
   public sealed class XFCalculatorFactory {
      public static XFCalculator BuildCalculator(string calcPath) {
         var serializer = new XmlSerializer(typeof(XFCalculator));
         try {
            var calculator = default(XFCalculator);
            using (var fs = File.OpenRead(calcPath)) {
               calculator = serializer.Deserialize(fs) as XFCalculator;
               calculator.InitializeToolKit(new XFCalculatorToolKit());
               fs.Close();
            }
            return calculator;
         }
         catch (Exception ex) {
            throw new XFCalculatorException(string.Format("Unable to build calculator: {0}", ex.Message), ex);
         }
      }

      public static XFCalculator BuildCalculator(string calcPath, XFCalculatorVariable input) {
         var serializer = new XmlSerializer(typeof(XFCalculator));
         try {
            var calculator = default(XFCalculator);
            using (var fs = File.OpenRead(calcPath)) {
               calculator = serializer.Deserialize(fs) as XFCalculator;
               calculator.InitializeToolKit(new XFCalculatorToolKit());
               calculator.InitializeInput(input);
               fs.Close();
            }
            return calculator;
         }
         catch (Exception ex) {
            throw new XFCalculatorException(string.Format("Unable to build calculator: {0}", ex.Message), ex);
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
            throw new XFCalculatorException(string.Format("Unable to build calculator: {0}", ex.Message), ex);
         }
      }

   }
}