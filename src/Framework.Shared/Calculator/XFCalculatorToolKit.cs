using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XF {
   public class XFCalculatorToolKit {
      private readonly IDictionary<string, MethodInfo> _tools = new Dictionary<string, MethodInfo>();
      private readonly IDictionary<string, object> _addins = new Dictionary<string, object>();

      public void Initialize(List<XFCalculatorStep> steps) {
         GetType().GetMethods().ToList().ForEach(tool => _tools.Add(tool.Name, tool));
         steps.ForEach(step => {
            if (step.Executor.Tool == null) return;
            if (step.Executor.Tool.Addin == null) return;
            var addin = step.Executor.Tool.Addin;
            if (_addins.ContainsKey(addin.Name)) return;
            var type = Type.GetType(string.Format("{0}.{1}, {2}", addin.Namespace, addin.Name, addin.Assembly));
            var toolAddin = Activator.CreateInstance(type);
            _addins.Add(addin.Name, toolAddin);

            if (!_tools.ContainsKey(step.Executor.Tool.Name)) {
               var tool = ((IToolKitAddin)toolAddin).GetMethodByName(step.Executor.Tool.Name);
               _tools.Add(step.Executor.Tool.Name, tool);
            }
         });
      }

      public bool IsDecimalList(string name) {
         if (_tools[name].GetParameters().Length > 1) return false;

         var param = _tools[name].GetParameters().First();
         return param.ParameterType == typeof(List<decimal>);
      }

      public object InvokeTool(string name, object[] args) {
         if (!_tools.ContainsKey(name)) return null;
         var tool = _tools[name];
         var obj = _addins.ContainsKey(tool.DeclaringType.Name) ? _addins[tool.DeclaringType.Name] : this;
         return _tools[name].Invoke(obj, args);
      }

      public decimal MultiplyByPercent(decimal value, decimal percent) {
         return value * (percent / 100m);
      }

      public decimal Add(List<decimal> values) {
         if (values == null) throw new ArgumentNullException();

         return values.Sum();
      }

      public decimal Multiply(List<decimal> values) {
         if (values == null) throw new ArgumentNullException();

         decimal product = 1; // multiplicative identity
         values.ForEach(x => product *= x);
         return product;
      }

      public decimal Subtract(decimal minuend, decimal subtrahend) {
         return minuend - subtrahend;
      }

      public decimal Divide(decimal dividend, decimal divisor) {
         if (divisor == 0m) throw new DivideByZeroException();

         return dividend / divisor;
      }

      public decimal Max(List<decimal> values) {
         if (values == null) throw new ArgumentNullException();

         return values.Max();
      }

      public decimal Min(List<decimal> values) {
         if (values == null) throw new ArgumentNullException();

         return values.Min();
      }

      public bool GE(decimal operand1, decimal operand2) {
         return (operand1 >= operand2);
      }

      public bool GT(decimal operand1, decimal operand2) {
         return (operand1 > operand2);
      }

      public bool LE(decimal operand1, decimal operand2) {
         return (operand1 <= operand2);
      }

      public bool LT(decimal operand1, decimal operand2) {
         return (operand1 < operand2);
      }

      public bool EQ(decimal operand1, decimal operand2) {
         return (operand1 == operand2);
      }

      public bool NQ(decimal operand1, decimal operand2) {
         return (operand1 != operand2);
      }

      public bool AND(bool operand1, bool operand2) {
         return (operand1 && operand2);
      }

      public bool OR(bool operand1, bool operand2) {
         return (operand1 || operand2);
      }

      public bool NOT(bool operand) {
         return !operand;
      }

   }
}