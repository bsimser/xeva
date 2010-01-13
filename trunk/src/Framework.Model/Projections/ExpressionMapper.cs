using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace XF.Model
{
   public class ExpressionMapper<TMapper, TEntity> : IExpressionMapper where TMapper : IEntityMapper
   {
      private readonly TMapper _mapper;
      private readonly List<string> _criteriaList = new List<string>();


      public ExpressionMapper(TMapper mapper)
      {
         _mapper = mapper;
      }

      public List<string> CriteriaList
      {
         get { return _criteriaList; }
      }

      public string ConjoinWith { get; set; }
 
      public string EntityName { get; set; }

      public IDictionary<string, object> CriteriaParameters
      {
         get { return _mapper.CriteriaParameters; }
         set { _mapper.CriteriaParameters = value; }
      }

      public ExpressionMapper<TMapper, TEntity> AddWithOr()
      {
         ConjoinWith = ExpressionConjunction.Or.ToString(); 
         return this;
      }

      public ExpressionMapper<TMapper, TEntity> AddWithAnd()
      {
         ConjoinWith = ExpressionConjunction.And.ToString();
         return this;
      }

      public ExpressionPart<ExpressionMapper<TMapper, TEntity>> Where(Expression<Func<TEntity, object>> entityColumn)
      {
         var property = ExpressionsHelper.GetMemberInfo(entityColumn) as PropertyInfo;
         if (property == null) return null;

         var criteria = string.Format("{0}.{1} ", EntityName.ToLower(), property.Name);

         var expressionPart = new ExpressionPart<ExpressionMapper<TMapper, TEntity>>(this, property.Name, criteria);
         return expressionPart;
      }

      public ExpressionPart<ExpressionMapper<TMapper, TEntity>> And(Expression<Func<TEntity, object>> entityColumn)
      {
         var property = ExpressionsHelper.GetMemberInfo(entityColumn) as PropertyInfo;
         if (property == null) return null;

         var criteria = string.Format(" {2} {0}.{1} ", EntityName.ToLower(), property.Name, _criteriaList.IsNotEmpty() ? "and" : string.Empty);

         var expressionPart = new ExpressionPart<ExpressionMapper<TMapper, TEntity>>(this, property.Name, criteria);
         return expressionPart;
      }

      public ExpressionPart<ExpressionMapper<TMapper, TEntity>> Or(Expression<Func<TEntity, object>> entityColumn)
      {
         var property = ExpressionsHelper.GetMemberInfo(entityColumn) as PropertyInfo;
         if (property == null) return null;

         var criteria = string.Format(" {2} {0}.{1} ", EntityName.ToLower(), property.Name, _criteriaList.IsNotEmpty() ? "or" : string.Empty);

         var expressionPart = new ExpressionPart<ExpressionMapper<TMapper, TEntity>>(this, property.Name, criteria);
         return expressionPart;
      }

      public TMapper AddCriteria()
      {
         return _mapper;
      }

   }

   public enum ExpressionConjunction
   {
      And, Or
   }

}