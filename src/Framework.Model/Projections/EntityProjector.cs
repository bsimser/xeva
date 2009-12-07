using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using NHibernate;

namespace XF.Model
{
   public class EntityProjector<TEntity, TMessage> : IProjector
   {
      private readonly ProjectionPart _parameters = new ProjectionPart();
      private readonly List<IReferencePart> _references = new List<IReferencePart>();
      private readonly ReferenceExpression _expressions = new ReferenceExpression();

      private readonly List<TMessage> _messages = new List<TMessage>();
      private readonly QueryRepository _queryRepository;

      public ReferenceExpression Expressions
      {
         get { return _expressions; }
      }

      public EntityProjector()
      {
         _queryRepository = QueryRepository.Instance;
      }

      public int ParameterIdx { get; set; }
      public int JoinRefIdx { get; set; }
      public int EntityLevel { get { return 0; } }

      public EntityProjector<TEntity, TMessage> Key(Expression<Func<TEntity, object>> keyExpression,
                                                   Expression<Func<TMessage, object>> messageExpression)
      {
         var keyProperty = ExpressionsHelper.GetMemberInfo(keyExpression) as PropertyInfo;
         var messageProperty = ExpressionsHelper.GetMemberInfo(messageExpression) as PropertyInfo;

         if (keyProperty == null) return this;
         if (messageProperty == null) return this;

         _parameters.Add(new ProjectionPart
         {
            MessageProperty = messageProperty,
            EntityProperty = keyProperty.Name,
            EntityName = string.Format("{0}_{1}", typeof(TEntity).Name, JoinRefIdx),
            ParameterIdx = ParameterIdx++,
            IsKey = true
         });
         return this;
      }

      public ParameterMapper<EntityProjector<TEntity, TMessage>, TEntity, TMessage> Projection(Expression<Func<TMessage, object>> messageExpression)
      {
         var messageProperty = ExpressionsHelper.GetMemberInfo(messageExpression) as PropertyInfo;
         var mapper = new ParameterMapper<EntityProjector<TEntity, TMessage>, TEntity, TMessage>(this, messageProperty);
         return mapper;
      }

      public ReferenceMapper<EntityProjector<TEntity, TMessage>, TRefEntity, TRefMessage> Reference<TRefEntity, TRefMessage>
         (Expression<Func<TEntity, object>> referencePath, Expression<Func<TMessage, object>> messageProperty)
      {
         var path = referencePath.Body.ToString();
         var entity = referencePath.Parameters[0].Type.Name.ToLower();
         var messageInfo = ExpressionsHelper.GetMemberInfo(messageProperty) as PropertyInfo;

         JoinRefIdx++;
         var mapper = new ReferenceMapper<EntityProjector<TEntity, TMessage>, TRefEntity, TRefMessage>(this, messageInfo, entity, path) 
         { JoinRefIdx = JoinRefIdx, EntityLevel = JoinRefIdx};
         return mapper;
      }

      public EntityProjector<TEntity, TMessage> Where(Expression<Func<TEntity, object>> entityExpression,
                                                     ReferenceExpressionOperator expressionOperator,
                                                     object value)
      {
         var messageProperty = ExpressionsHelper.GetMemberInfo(entityExpression) as PropertyInfo;

         if (messageProperty == null) return this;

         var entityName = typeof(TEntity).Name;
         _expressions.Add(new ReferenceExpression
         {
            PropertyName = messageProperty.Name,
            PropertyPath = entityName.ToLower(),
            EntityName = string.Format("{0}_{1}", entityName, 0),
            Operator = expressionOperator,
            Value = value
         });
         return this;
      }

      public void AddParameterPart(ProjectionPart parameterPart)
      {
         _parameters.Add(parameterPart);
      }

      public void AddReferencePart(IReferencePart referencePart)
      {
         _references.Add(referencePart);
      }

      public List<TMessage> Project()
      {
         try
         {
            var iQuery = _queryRepository.GetQueryFor(typeof(TMessage), typeof(TEntity), _parameters, _references, _expressions);
            SetQueryParameterValues(iQuery);

            CreateOutputProjections(iQuery.Enumerable());
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
         }

         return _messages;
      }

      private void SetQueryParameterValues(IQuery query)
      {
         _expressions.ForEach(exp => exp.SetParameter(query));
         _references.ForEach(refpart => refpart.SetPartParameters(query));
      }

      private void CreateOutputProjections(IEnumerable enumerable)
      {
         var keyIdx = _parameters.FindIndex(match => match.IsKey);
         var results = new Dictionary<object, TMessage>();

         foreach (object[] tuple in enumerable)
         {
            var keyValue = (Guid)tuple[keyIdx];

            var result = results.ContainsKey(keyValue) ? results[keyValue] : Activator.CreateInstance<TMessage>();
            _parameters.ForEach(param => param.SetOutputValue(result, tuple));
            _references.ForEach(reference => reference.GenerateOutputReference(result, tuple));

            if (results.ContainsKey(keyValue))
               results[keyValue] = result;
            else
               results.Add(keyValue, result);
         }

         foreach (var valuePair in results)
         {
            _messages.Add(valuePair.Value);
         }
      }
   }
}