using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XF {
   [XmlRoot("XFCalculator", Namespace = "http://tempuri.org/XFCalculator.xsd")]
   public class XFCalculator {
      private readonly IDictionary<string, object> _results = new Dictionary<string, object>();

      [XmlAttribute(AttributeName = "version", DataType = "string")]
      public string Version { get; set; }

      [XmlAttribute(AttributeName = "name", DataType = "string")]
      public string Name { get; set; }

      [XmlAttribute(AttributeName = "rounding", DataType = "string")]
      public string Rounding { get; set; }

      [XmlElement("algorithmobject", Type = typeof(XFCalculatorAlgorithm))]
      public XFCalculatorAlgorithm Algorithm { get; set; }

      public void InitializeInput(XFCalculatorVariable input) {
         Algorithm.Input = input;
      }

      public void InitializeToolKit(XFCalculatorToolKit toolKit) {
         Algorithm.ToolKit = toolKit;
      }

      public IDictionary<string, object> Run() {
         while (Algorithm.NextStep) {
            var stepResult = Algorithm.ExecuteStep();
            if (string.IsNullOrEmpty(stepResult.Key)) continue;
            _results.Add(stepResult);
         }
         return _results;
      }
   }

   public enum XFCalculatorRound {
      ToEven, AwayFromZero, ToWhole, None, TruncateToWhole
   }
}