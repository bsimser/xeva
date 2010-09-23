using System.Collections.Generic;
using System.Xml.Serialization;

namespace XF {
   [XmlRoot("controlflow")]
   public class XFControlFlow : XFExecutionBase {
      public override object Execute(XFCalculatorToolKit toolKit, IDictionary<string, object> variables) {
         var assert = base.Execute(toolKit, variables);
         return assert.ToString() == bool.TrueString ? SkipTo : string.Empty;
      }
   }
}