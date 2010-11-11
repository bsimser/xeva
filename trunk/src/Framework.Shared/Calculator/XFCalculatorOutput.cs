using System.Xml.Serialization;

namespace XF {
   public class XFCalculatorOutput {
      [XmlAttribute(AttributeName = "name", DataType = "string")]
      public string Name { get; set; }

      public object Value { get; set; }
      public object Reference { get; set; }
   }
}