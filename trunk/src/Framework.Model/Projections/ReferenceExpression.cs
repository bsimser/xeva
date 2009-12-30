using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System;
using NHibernate;

namespace XF.Model
{
   public class ReferenceExpression : List<ReferenceExpression>
   {
      public string EntityName { get; set; }
      public string PropertyName { get; set; }
      public PropertyInfo Property { get; set; }
      public ReferenceExpressionOperator Operator { get; set; }
      public object Value { get; set; }

      public string GetWherePart()
      {
         var result = new StringBuilder(Environment.NewLine);
         result.Append(string.Format("{0}.{1} {2} :{3}{1} and",
                                     EntityName.ToLower(),
                                     PropertyName,
                                     GetOperatorFromExpression(Operator),
                                     EntityName.ToLower()));
         return result.ToString();
      }

      private string GetOperatorFromExpression(ReferenceExpressionOperator expressionOperator)
      {
         switch (expressionOperator)
         {
            case ReferenceExpressionOperator.Equal:
               return "=";
            case ReferenceExpressionOperator.GT:
               return ">";
            case ReferenceExpressionOperator.LT:
               return "<";
            case ReferenceExpressionOperator.Starts:
               return "=";
            case ReferenceExpressionOperator.In:
               return "in";
         }
         return "";
      }

      public void SetParameter(IQuery query)
      {
         query.SetParameter(string.Format("{0}{1}", EntityName.ToLower(), PropertyName), Value);
      }
   }

   public enum ReferenceExpressionOperator
   {
      Equal, In, GT, LT, Starts
   }
}