using System.Linq.Expressions;
using System.Reflection;

namespace XF
{
   public static class ExpressionsHelper
   {
      public static MemberInfo GetMemberInfo(LambdaExpression expression)
      {
         MemberExpression memberExpression = null;
         if (expression.Body.NodeType == ExpressionType.Convert) {
            memberExpression = ((UnaryExpression)expression.Body).Operand as MemberExpression;
         } else if (expression.Body.NodeType == ExpressionType.MemberAccess) {
            memberExpression = expression.Body as MemberExpression;
         }
         return memberExpression != null ? memberExpression.Member : null;
      }
   }
}