using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace XF.Model {
   public class ComputationMapper<TMapper, TEntity, TMessage>
            where TMapper : IEntityMapper, IArgumentSource {
      private readonly List<string> _namedArguments = new List<string>();

      public ComputationMapper(ParameterMapper<TMapper, TEntity, TMessage> mapper) {
         Mapper = mapper;
      }

      private ParameterMapper<TMapper, TEntity, TMessage> Mapper { get; set; }

      public ParameterMapper<TMapper, TEntity, TMessage> AddComputation() {
         Mapper.NamedArguments = _namedArguments;
         return Mapper;
      }

      public ComputationMapper<TMapper, TEntity, TMessage> NamedArgument<TArg>(Expression<Func<TArg, object>> argExpression) {
         var argProperty = ExpressionsHelper.GetMemberInfo(argExpression) as PropertyInfo;
         _namedArguments.Add(string.Format("{0}.{1}", typeof (TArg).Name, argProperty.Name));
         return this;
      }
   }
}