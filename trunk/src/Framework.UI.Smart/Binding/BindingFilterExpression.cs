using System.Collections.Generic;
using System.Linq;

namespace XF.UI.Smart
{
   public class BindingFilterExpression
   {
      private string _op;
      private string _property;
      private List<string> _expressionValues;
      private bool _isMatch;

      public BindingFilterExpression(string property, string op, List<string> values)
      {
         _property = property;
         _op = op;
         _expressionValues = values;
      }

      public string Property
      {
         get { return _property; }
         set { _property = value; }
      }

      public string Operator
      {
         get { return _op; }
         set { _op = value; }
      }

      public List<string> Values
      {
         get { return _expressionValues; }
         set { _expressionValues = value; }
      }

      public bool IsMatch
      {
         get { return _isMatch; }
         set { _isMatch = value; }
      }

      public void EvaluateExpression(string compareValues)
      {
         switch (Operator.ToUpper())
         {
            case "EQUAL":
               IsMatch = _expressionValues[0] == (compareValues != null ? compareValues.Replace("\n", "").Replace("\r", "") : "null");
               break;
            case "=":
               IsMatch = _expressionValues[0] == (compareValues != null ? compareValues.Replace("\n", "").Replace("\r", "") : "null");
               break;
            case "!=":
               IsMatch = _expressionValues[0] != (compareValues != null ? compareValues.Replace("\n", "").Replace("\r", "") : "null");
               break;
            case "NOT IN":
               //IsMatch = !_expressionValues.Contains(compareValues);
               IsMatch = !_expressionValues.Intersect(compareValues.Split(',')).Any();
               break;
            case "IN":
               //IsMatch = _expressionValues.Contains(compareValues);
               IsMatch = _expressionValues.Intersect(compareValues.Split(',')).Any();
               break;
            case "LIKE":
               IsMatch = compareValues.Contains(_expressionValues[0]);
               break;
            default:
               IsMatch = false;
               break;
         }
      }
   }
}