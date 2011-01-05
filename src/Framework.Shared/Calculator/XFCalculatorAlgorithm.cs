using System.Collections.Generic;
using System.Xml.Serialization;

namespace XF {
   [XmlRoot("AlgorithmObject")]
   public class XFCalculatorAlgorithm {
      private List<XFCalculatorStep> _steps = new List<XFCalculatorStep>();
      private readonly IDictionary<string, object> _variables = new Dictionary<string, object>();
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
            if(value == null) return;
            foreach (var pair in value.Variables) {
               if(!_variables.ContainsKey(pair.Key))
                  _variables.Add(pair.Key, pair.Value);
            }
         }
      }

      public XFCalculatorToolKit ToolKit {
         set {
            _toolKit = value;
            _toolKit.Initialize(Steps);
         }
      }

      public void AssignInputVariable(string key, object value) {
         if (!_variables.ContainsKey(key))
            _variables.Add(key, value);
      }

      public void SkipTo(string tag) {
         if (string.IsNullOrEmpty(tag)) return;
         var skipIdx = _steps.FindIndex(_idx+1, step => step.Tag.Equals(tag));
         if (skipIdx == -1) return;
         for (var i = _idx+1; i < skipIdx; i++) {
            _steps[i].IsSkipped = true;
         }
         // decrement skipIdx so NextStep's _idx++ positions index on correct step
         _idx = --skipIdx ;
      }

      public KeyValuePair<string, object> ExecuteStep() {
         var pair = Step.Compute(_variables, _toolKit);
         _variables.Add(pair);

         if (Step.Executor is XFControlFlow) SkipTo(pair.Value.ToString());

         return pair;
      }

      public void RemoveStep(string stepName) {
         _steps.RemoveAll(m => m.Tag == stepName);
      }
   }
}