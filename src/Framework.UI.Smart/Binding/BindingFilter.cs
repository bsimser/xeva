using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace XF.UI.Smart
{
   public class BindingFilter<FilteredType> : IBindingFilter<FilteredType>
   {
      private string _filterString;
      private List<BindingFilterExpression> _expressions;
      private List<PropertyDescriptor> _properties;

      public BindingFilter()
      {
         _expressions = new List<BindingFilterExpression>();
         _properties = new List<PropertyDescriptor>();
      }

      public string FilterString
      {
         get { return _filterString; }
         set { _filterString = value; }
      }

      public List<PropertyDescriptor> Properties
      {
         get { return _properties; }
      }

      public void Initialize(string filterString)
      {
         _filterString = filterString;
         ExtractExpressionsFromFilterString();
         ExtractPropertiesFromFilteredType();
      }

      public bool IncludeItem(FilteredType item)
      {
         foreach (var descriptor in _properties)
         {
             _expressions.ForEach(exp =>
                                      {
                                          var itemValue = descriptor.GetValue(item) != null
                                                              ? descriptor.GetValue(item).ToString()
                                                              : null;
                                          exp.EvaluateExpression(itemValue);
                                      });           
         }
         var nonMatchedExpression = _expressions.FindAll(compare => !compare.IsMatch);
         return nonMatchedExpression.Count == 0;
      }

      private void ExtractPropertiesFromFilteredType()
      {
         _properties = new List<PropertyDescriptor>();
         foreach (var expression in _expressions)
         {
            var descriptor = TypeDescriptor.GetProperties(typeof(FilteredType))[expression.Property];
            if (!_properties.Exists(match=> match.Name==descriptor.Name))
                _properties.Add(descriptor);
         }
      }

      private void ExtractExpressionsFromFilterString()
      {
         _expressions = new List<BindingFilterExpression>();

         var entries = _filterString.Split(new string[3] { "and", "And", "AND" }, 100, StringSplitOptions.RemoveEmptyEntries);
         foreach (var entry in entries)
         {
            var splitEntry = new string[3];
            splitEntry = ExtractFilterProperty(entry);

            var property = splitEntry[0];
            var op = splitEntry[1];
            var arguments = splitEntry[2].Split(',');
            var args = GetArgumentValues(arguments, 0);

            var expression = new BindingFilterExpression(property, op, args);
            _expressions.Add(expression);
         }
      }

      private List<string> GetArgumentValues(string[] arguments, int startPos)
      {
         var results = new List<string>();
         for (var idx = startPos; idx < arguments.Length; idx++)
         {
            var argValue = arguments[idx].Replace("'", "").Replace("(", "").Replace(")", "").Replace(",", "");
            results.Add(argValue.Trim());
         }

         return results;
      }

      private readonly string[] _operators = new[] { " EQUAL ", " = ", " != ", " IN ", " NOT IN ", " LIKE ", " <= ", " >= "};
      private string[] ExtractFilterProperty(string entry)
      {
         var result = new string[3];

         var operatorIndex = 1000;
         foreach (var op in _operators)
         {
            if (entry.IndexOf(op) != -1 && entry.IndexOf(op) < operatorIndex)
            {
               operatorIndex = entry.IndexOf(op);
               result[1] = op.Trim();
            }
         }
         result[0] = entry.Substring(0, operatorIndex).Trim();
         result[2] = entry.Substring(operatorIndex + result[1].Length + 2, entry.Length - (operatorIndex + result[1].Length + 2)).Trim();
         return result;
      }

   }
}