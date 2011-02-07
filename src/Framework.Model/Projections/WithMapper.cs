using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace XF.Model {
   public class WithMapper<TMapper, TEntity> : IExpressionMapper where TMapper : IJoinMapper {
      private readonly TMapper _mapper;
      private List<string> _criteriaList = new List<string>();

      public WithMapper(TMapper mapper) {
         _mapper = mapper;
      }

      public string EntityName { get; set; }
      public List<string> CriteriaList {
         get { return _criteriaList; }
         private set { _criteriaList = value; }
      }

      public IDictionary<string, object> CriteriaParameters {
         get { return _mapper.CriteriaParameters; }
         set { _mapper.CriteriaParameters = value; }
      }

      public string ConjoinWith { get; set; }

      public ExpressionPart<WithMapper<TMapper, TEntity>> Where(Expression<Func<TEntity, object>> entityColumn) {
         var property = ExpressionsHelper.GetMemberInfo(entityColumn) as PropertyInfo;
         if (property == null) return null;

         var criteria = string.Format("{0}.{1} ", EntityName.ToLower(), property.Name);

         var expressionPart = new ExpressionPart<WithMapper<TMapper, TEntity>>(this, property.Name, criteria);
         return expressionPart;
      }

      public ExpressionPart<WithMapper<TMapper, TEntity>> And(Expression<Func<TEntity, object>> entityColumn) {
         var property = ExpressionsHelper.GetMemberInfo(entityColumn) as PropertyInfo;
         if (property == null) return null;

         var criteria = string.Format(" {2} {0}.{1} ", EntityName.ToLower(), property.Name, _criteriaList.IsNotEmpty() ? "and" : string.Empty);

         var expressionPart = new ExpressionPart<WithMapper<TMapper, TEntity>>(this, property.Name, criteria);
         return expressionPart;
      }

      public ExpressionPart<WithMapper<TMapper, TEntity>> Or(Expression<Func<TEntity, object>> entityColumn) {
         var property = ExpressionsHelper.GetMemberInfo(entityColumn) as PropertyInfo;
         if (property == null) return null;

         var criteria = string.Format(" {2} {0}.{1} ", EntityName.ToLower(), property.Name, _criteriaList.IsNotEmpty() ? "or" : string.Empty);

         var expressionPart = new ExpressionPart<WithMapper<TMapper, TEntity>>(this, property.Name, criteria);
         return expressionPart;
      }


      public TMapper AddWith() {
         return _mapper;
      }

   }
}