using System.Collections.Generic;

namespace XF {
   public class XFCalculatorVariable {
      private readonly IDictionary<string, object> _variables = new Dictionary<string, object>();

      public IDictionary<string, object> Variables {
         get { return _variables; }
      }

      public XFCalculatorVariable AddVariable(string key, object value) {
         _variables.Add(key, value);
         return this;
      }

   }
}