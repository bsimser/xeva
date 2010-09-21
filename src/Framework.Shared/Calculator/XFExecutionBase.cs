using System.Collections.Generic;
using System.Xml.Serialization;

namespace Model {
   public abstract class XFExecutionBase {
      private List<XFCalculatorInput> _inputs = new List<XFCalculatorInput>();

      [XmlElement("input")]
      public List<XFCalculatorInput> Inputs {
         get { return _inputs; }
         set { _inputs = value; }
      }

      [XmlElement("output")]
      public string Output { get; set; }

      [XmlElement("skipTo")]
      public string SkipTo { get; set; }

      [XmlElement("tool")]
      public XFCalculatorTool Tool { get; set; }

      [XmlElement("comment")]
      public string Comment { get; set; }

      public virtual object Execute(XFCalculatorToolKit toolKit, IDictionary<string, object> variables) {
         var isDecimalList = toolKit.IsDecimalList(Tool.Name);
         var args = isDecimalList ? PrepareDecimalListArgument(Inputs, variables)
                                  : PrepareDiscreteArguments(Inputs, variables);
         return toolKit.InvokeTool(Tool.Name, args);
      }

      protected object[] PrepareDecimalListArgument(List<XFCalculatorInput> inputs, IDictionary<string, object> variables) {
         try {
            inputs.Sort((a, b) => a.Parameter.CompareTo(b.Parameter));
            var list = new List<decimal>();
            inputs.ForEach(inp => list.Add((decimal)variables[inp.Name]));
            return new[] { list };
         }
         catch (KeyNotFoundException ex) {
            throw (ex);
         }
      }

      protected object[] PrepareDiscreteArguments(List<XFCalculatorInput> inputs, IDictionary<string, object> variables) {
         inputs.Sort((a, b) => a.Parameter.CompareTo(b.Parameter));
         var results = new List<object>();
         inputs.ForEach(inp => results.Add(variables[inp.Name]));
         return results.ToArray();
      }

   }
}