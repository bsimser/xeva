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
         foreach (PropertyDescriptor descriptor in _properties)
         {
            BindingFilterExpression expression = _expressions.Find(delegate(BindingFilterExpression match)
                                                                      {
                                                                         return match.Property == descriptor.Name;
                                                                      });
            if (expression != null)
            {
               string itemValue = descriptor.GetValue(item) != null ? descriptor.GetValue(item).ToString() : null;
               expression.EvaluateExpression(itemValue);
            }
         }

         List<BindingFilterExpression> nonMatchedExpression =
            _expressions.FindAll(delegate(BindingFilterExpression compare)
                                    { return !compare.IsMatch; });

         return nonMatchedExpression.Count == 0;
      }

      private void ExtractPropertiesFromFilteredType()
      {
         _properties = new List<PropertyDescriptor>();
         foreach (BindingFilterExpression expression in _expressions)
         {
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(typeof(FilteredType))[expression.Property];
            _properties.Add(descriptor);
         }
      }

      private void ExtractExpressionsFromFilterString()
      {
         _expressions = new List<BindingFilterExpression>();

         string[] entries = _filterString.Split(new string[3] { "and", "And", "AND" }, 100, StringSplitOptions.RemoveEmptyEntries);
         foreach (string entry in entries)
         {
            string[] splitEntry = new string[3];
            splitEntry = ExtractFilterProperty(entry);

            string property = splitEntry[0];
            string op = splitEntry[1];
            string[] arguments = splitEntry[2].Split(',');
            List<string> args = GetArgumentValues(arguments, 0);

            BindingFilterExpression expression = new BindingFilterExpression(property, op, args);
            _expressions.Add(expression);
         }
      }

      private List<string> GetArgumentValues(string[] arguments, int startPos)
      {
         List<string> results = new List<string>();
         for (int idx = startPos; idx < arguments.Length; idx++)
         {
            string argValue = arguments[idx].Replace("'", "").Replace("(", "").Replace(")", "").Replace(",", "");
            results.Add(argValue.Trim());
         }

         return results;
      }

      private string[] _operators = new string[] { " EQUAL ", " = ", " != ", " IN ", " NOT IN ", " LIKE " };
      private string[] ExtractFilterProperty(string entry)
      {
         string[] result = new string[3];

         int operatorIndex = 1000;
         foreach (string op in _operators)
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