using System.Collections.Generic;
using System.Xml.Serialization;

namespace XF {
   [XmlRoot("step")]
   public class XFCalculatorStep {
      [XmlAttribute(AttributeName = "name", DataType = "string")]
      public string Name { get; set; }

      [XmlAttribute(AttributeName = "can-exclude", DataType = "boolean")]
      public bool CanExclude { get; set; }

      public string Tag {
         get { return Executor != null ? Executor.Output : string.Empty; }
      }

      private XFExecutionBase _computation;

      public XFExecutionBase Executor {
         get { return _computation; }
      }

      [XmlElement("computation", typeof(XFComputation))]
      public XFComputation Computation {
         get { return _computation as XFComputation; }
         set { _computation = value; }
      }

      [XmlElement("controlflow", typeof(XFControlFlow))]
      public XFControlFlow ControlFlow {
         get { return _computation as XFControlFlow; }
         set { _computation = value; }
      }

      public KeyValuePair<string, object> Compute(IDictionary<string, object> variables, XFCalculatorToolKit toolKit) {
         var computationResult =  _computation is XFComputation
                                        ? Computation.Execute(toolKit, variables)
                                        : ControlFlow.Execute(toolKit, variables);
         return new KeyValuePair<string, object>(_computation.Output, computationResult);
      }
      
   }
}