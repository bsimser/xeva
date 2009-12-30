using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using NHibernate;

namespace XF.Model
{
   public class EntityProjector<TEntity, TMessage> : IProjector, IEntityMapper
   {
      private readonly ProjectionPart _parameters = new ProjectionPart();

      private readonly List<IReferencePart> _references = new List<IReferencePart>();
      private readonly ReferenceExpression _expressions = new ReferenceExpression();
      private IDictionary<string, object> _criteriaParameters = new Dictionary<string, object>();

      private readonly List<TMessage> _messages = new List<TMessage>();
      private readonly QueryRepository _queryRepository;
      private readonly List<IExpressionMapper> _citerion = new List<IExpressionMapper>();

      public EntityProjector()
      {
         _queryRepository = new QueryRepository(UnitOfWork.Store);
      }

      public List<IExpressionMapper> Citerion
      {
         get { return _citerion; }
      }

      public IDictionary<string, object> CriteriaParameters
      {
         get { return _criteriaParameters; }
         set { _criteriaParameters = value; }
      }

      public ReferenceExpression Expressions
      {
         get { return _expressions; }
      }

      public ProjectionPart Parameters
      {
         get { return _parameters; }
      }

      public List<IReferencePart> References
      {
         get { return _references; }
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

      public ReferenceMapper<EntityProjector<TEntity, TMessage>, TRefEntity, TRefMessage> Reference<TRefEntity, TRefMessage>
         (Expression<Func<TEntity, object>> referencePath)
      {
         var path = referencePath.Body.ToString();
         var entity = referencePath.Parameters[0].Type.Name.ToLower();

         JoinRefIdx++;
         var mapper = new ReferenceMapper<EntityProjector<TEntity, TMessage>, TRefEntity, TRefMessage>(this, null, entity, path) 
         { JoinRefIdx = JoinRefIdx, EntityLevel = JoinRefIdx };
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
            EntityName = string.Format("{0}_{1}", entityName, 0),
            Operator = expressionOperator,
            Value = value
         });
         return this;
      }

      public ExpressionMapper<EntityProjector<TEntity, TMessage>, TEntity> Criteria()
      {
         var entityName = typeof(TEntity).Name;
         var mapper = new ExpressionMapper<EntityProjector<TEntity, TMessage>, TEntity>(this) 
               { EntityName = string.Format("{0}_{1}", entityName, 0) };

         Citerion.Add(mapper);
         return mapper;
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
            var iQuery = _queryRepository.GetQueryFor(typeof(TMessage), typeof(TEntity), this);
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
         if (_criteriaParameters.IsEmpty())
         {
            _expressions.ForEach(exp => exp.SetParameter(query));
            _references.ForEach(refpart => refpart.SetPartParameters(query));
         }
         else
            foreach (var criterion in _criteriaParameters)
               query.SetParameter(criterion.Key, criterion.Value);
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