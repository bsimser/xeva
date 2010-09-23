using System.Xml.Serialization;

namespace XF {
   [XmlRoot("Input")]
   public class XFCalculatorInput {
      [XmlAttribute(AttributeName = "name", DataType = "string")]
      public string Name { get; set; }

      [XmlAttribute(AttributeName = "parameter", DataType = "string")]
      public string Parameter { get; set; }
   }
}