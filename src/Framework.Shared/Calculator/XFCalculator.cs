using System.Collections.Generic;
using System.Xml.Serialization;

namespace Model {
   [XmlRoot("Algorithm", Namespace = "http://tempuri.org/Algorithm.xsd")]
   public class XFCalculator {
      private readonly IDictionary<string, object> _results = new Dictionary<string, object>();

      [XmlElement("algorithmobject", Type = typeof(Algorithm))]
      public Algorithm Algorithm { get; set; }

      internal void Initialize(XFCalculatorVariable input, XFCalculatorToolKit toolKit) {
         Algorithm.Input = input;
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

   [XmlRoot("AlgorithmObject")]
   public class Algorithm {
      private List<XFCalculatorStep> _steps = new List<XFCalculatorStep>();
      private IDictionary<string, object> _variables = new Dictionary<string, object>();
      private XFCalculatorToolKit _toolKit;
      private int _idx = -1;

      [XmlElement("step", Type = typeof(XFCalculatorStep))]
      public List<XFCalculatorStep> Steps {
         get { return _steps; }
         set { _steps = value; }
      }

      public XFCalculatorStep Step {
         get { return _idx < _steps.Count && _idx > -1 ? _steps[_idx] : null; }
      }

      public bool NextStep {
         get {
            _idx++;
            return _idx < _steps.Count;
         }
      }

      public XFCalculatorVariable Input {
         set {
            _variables = value.Variables;
         }
      }

      public XFCalculatorToolKit ToolKit {
         set {
            _toolKit = value;
            _toolKit.Initialize(Steps);
         }
      }

      public void SkipForward(string tag) {
         if (string.IsNullOrEmpty(tag)) return;

         var skip = _steps.Find(step => step.Tag.Equals(tag));
         if (skip != null) {
            var skipIdx = _steps.IndexOf(skip);
            if (skipIdx >= _idx)
               _idx = _steps.IndexOf(skip) - 1;
         }
      }

      public KeyValuePair<string, object> ExecuteStep() {
         var pair = Step.Compute(_variables, _toolKit);
         _variables.Add(pair);

         if (Step.Executor is XFControlFlow) SkipForward(pair.Value.ToString());

         return pair;
      }
   }
}