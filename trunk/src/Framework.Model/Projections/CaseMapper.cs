using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace XF.Model {
   public class CaseMapper<TMapper, TEntity> : IExpressionMapper 
         where TMapper : IParameterMapper {
      private readonly List<string> _namedArguments = new List<string>();

      public CaseMapper(TMapper mapper) {
         Mapper = mapper;
      }

      private TMapper Mapper { get; set; }

      public IDictionary<string, object> CriteriaParameters { get; set; }
      public string EntityName { get; set; }
      public List<string> CriteriaList { get; private set; }
      public string ConjoinWith { get; set; }

      public TMapper AddCase() {
         //Mapper.NamedArguments = _namedArguments;
         return Mapper;
      }

      public ExpressionPart<CaseMapper<TMapper, TEntity>> When(Expression<Func<TEntity, object>> entityExpression) {
         var entityProperty = ExpressionsHelper.GetMemberInfo(entityExpression) as PropertyInfo;
         var entityName = typeof(TEntity).Name;
         var expressionPart = new ExpressionPart<CaseMapper<TMapper, TEntity>>(this, entityProperty.Name, null);
         return expressionPart;
      }

      //public CaseMapper<TMapper, TEntity, TMessage> NamedArgument<TArg>(Expression<Func<TArg, object>> argExpression) {
      //   var argProperty = ExpressionsHelper.GetMemberInfo(argExpression) as PropertyInfo;
      //   _namedArguments.Add(string.Format("{0}.{1}", typeof(TArg).Name, argProperty.Name));
      //   return this;
      //}

   }

   public class CaseExpressionPart<TMapper, TEntity> {
   }

   public interface ICaseMapper {
   }

   public class WhenMapper<TMapper, TEntity, TMessage>
         where TMapper : ICaseMapper {
      private readonly ICaseMapper _mapper;

      public WhenMapper(ICaseMapper mapper) {
         _mapper = mapper;
         throw new NotImplementedException();
      }

   }
}