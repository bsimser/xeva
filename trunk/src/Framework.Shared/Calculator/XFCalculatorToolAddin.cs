using System.Xml.Serialization;

namespace Model {
   [XmlRoot("ToolAddin")]
   public class XFCalculatorToolAddin {
      [XmlElement("addinName")]
      public string Name { get; set; }

      [XmlElement("addinNamespace")]
      public string Namespace { get; set; }

      [XmlElement("addinAssembly")]
      public string Assembly { get; set; }
   }
}