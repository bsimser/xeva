using System;
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
         compareValues = compareValues != null
                       ? compareValues.Replace("\n", "")
                                      .Replace("\r", "")
                                      .Replace(", ", ",")
                       : "null";
         switch (Operator.ToUpper())
         {
            case "EQUAL":
               IsMatch = _expressionValues[0] == compareValues;
               break;
            case "=":
               IsMatch = _expressionValues[0] == compareValues;
               break;
            case "!=":
               IsMatch = _expressionValues[0] != compareValues;
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
            case "<=":
               IsMatch = EvaluateComparison(compareValues);
               break;
            case ">=":
               IsMatch = EvaluateComparison(compareValues);
               break;
            default:
               IsMatch = false;
               break;
         }
      }

       private bool EvaluateComparison(string values)
       {
           DateTime evalDate;
           DateTime existingDate;
           if (DateTime.TryParse(values, out existingDate) && DateTime.TryParse(_expressionValues[0], out evalDate))
           {
               if (Operator=="<=")
                   return existingDate <= evalDate;
               if (Operator == ">=")
                   return existingDate >= evalDate;
           }

           Decimal evalNumber;
           Decimal existingNumber;
           if (Decimal.TryParse(values,out existingNumber) && Decimal.TryParse(_expressionValues[0],out evalNumber))
           {
               if (Operator == "<=")
                   return existingNumber <= evalNumber;
               if (Operator == ">=")
                   return existingNumber >= evalNumber;
           }
           return false;
       }
   }
}