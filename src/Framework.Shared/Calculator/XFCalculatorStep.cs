using System.Collections.Generic;
using System.Xml.Serialization;

namespace XF {
   [XmlRoot("step")]
   public class XFCalculatorStep {
      [XmlAttribute(AttributeName = "can-exclude", DataType = "boolean")]
      public bool CanExclude { get; set; }

      [XmlAttribute(AttributeName = "input-by-tag", DataType = "boolean")]
      public bool InputByTag { get; set; }

      [XmlAttribute(AttributeName = "is-a-total", DataType = "boolean")]
      public bool IsATotal { get; set; }

      [XmlAttribute(AttributeName = "exclude-output", DataType = "boolean")]
      public bool ExcludeOutput { get; set; }

      [XmlAttribute(AttributeName = "final-output", DataType = "boolean")]
      public bool FinalOutput { get; set; }

      public bool IsSkipped { get; set; }

      public string Tag {
         get { return Executor != null ? Executor.Output.Name : string.Empty; }
      }

      public XFExecutionBase Executor { get; set; }

      [XmlElement("computation", typeof(XFComputation))]
      public XFComputation Computation {
         get { return Executor as XFComputation; }
         set { Executor = value; }
      }

      [XmlElement("controlflow", typeof(XFControlFlow))]
      public XFControlFlow ControlFlow {
         get { return Executor as XFControlFlow; }
         set { Executor = value; }
      }

      public KeyValuePair<string, object> Compute(IDictionary<string, object> variables, XFCalculatorToolKit toolKit) {
         Executor.Output.Value =  Executor is XFComputation
                                        ? Computation.Execute(toolKit, variables)
                                        : ControlFlow.Execute(toolKit, variables);
         return Executor.KeyValue;
      }
      
   }
}