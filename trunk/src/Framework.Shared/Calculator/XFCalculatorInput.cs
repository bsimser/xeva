using System.Xml.Serialization;

namespace Model {
   [XmlRoot("Input")]
   public class XFCalculatorInput {
      [XmlAttribute(AttributeName = "name", DataType = "string")]
      public string Name { get; set; }

      [XmlAttribute(AttributeName = "parameter", DataType = "string")]
      public string Parameter { get; set; }
   }
}