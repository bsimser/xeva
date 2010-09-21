using System.Xml.Serialization;

namespace Model {
   [XmlRoot("Tool")]
   public class XFCalculatorTool {
      [XmlElement("toolName")]
      public string Name { get; set; }

      [XmlElement("toolAddin")]
      public XFCalculatorToolAddin Addin { get; set; }
   }
}