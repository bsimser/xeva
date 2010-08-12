using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using XF;

namespace XF.Model
{
   public class ReferenceMapper<TMapper, TEntity, TMessage> : IEntityMapper
      where TMapper : IEntityMapper
   {
      private readonly TMapper _projector;
      private readonly ProjectionPart _parameters = new ProjectionPart();
      private readonly List<IReferencePart> _references = new List<IReferencePart>();
      private readonly string _referencePath;
      private readonly string _rootType;
      private readonly PropertyInfo _subProjection;
      private ReferenceJoinType _joinType = ReferenceJoinType.none;
      private ReferenceType _referenceType = ReferenceType.PropertyPart;
      private bool _isKeyed;
      private PropertyInfo _keyProperty;

      public ReferenceMapper(TMapper projector, PropertyInfo messageInfo, string entity, string referencePath)
      {
         _projector = projector;
         _rootType = entity;
         _subProjection = messageInfo;

         var removeidx = referencePath.IndexOf('.') + 1;
         _referencePath = referencePath.Substring(removeidx, referencePath.Length - removeidx);
      }

      public int EntityLevel { get; set; }

      public int ParameterIdx
      {
         get { return _projector.ParameterIdx; }
         set { _projector.ParameterIdx = value; }
      }

      public int JoinRefIdx
      {
         get { return _projector.JoinRefIdx; }
         set { _projector.JoinRefIdx = value; }
      }

      public List<IExpressionMapper> Citerion
      {
         get { return _projector.Citerion; }
      }

      public List<IOrderingMapper> Ordering {
         get { return _projector.Ordering; }
      }

      public ReferenceMapper<TMapper, TEntity, TMessage> ReferenceAsProperty()
      {
         _referenceType = ReferenceType.PropertyPart;
         return this;
      }

      public ReferenceMapper<TMapper, TEntity, TMessage> ReferenceAsType() 
      {
         _referenceType = ReferenceType.TypePart;
         return this;
      }

      public ReferenceMapper<TMapper, TEntity, TMessage> ReferenceAsCollection() 
      {
         _referenceType = ReferenceType.CollectionPart;
         return this;
      }

      public ReferenceMapper<TMapper, TEntity, TMessage> Join()
      {
         _joinType = ReferenceJoinType.inner;
         return this;
      }

      public ReferenceMapper<TMapper, TEntity, TMessage> JoinLeft() 
      {
         _joinType = ReferenceJoinType.left;
         return this;
      }

      public ReferenceMapper<TMapper, TEntity, TMessage> JoinRight() 
      {
         _joinType = ReferenceJoinType.right;
         return this;
      }

      public ReferenceMapper<TMapper, TEntity, TMessage> Key(Expression<Func<TEntity, object>> keyExpression,
                                                             Expression<Func<TMessage, object>> messageExpression) {
         var keyProperty = ExpressionsHelper.GetMemberInfo(keyExpression) as PropertyInfo;
         var messageProperty = ExpressionsHelper.GetMemberInfo(messageExpression) as PropertyInfo;

         if (keyProperty == null) return this;
         if (messageProperty == null) return this;

         _isKeyed = true;
         _keyProperty = messageProperty;

         _parameters.Add(new ProjectionPart {
            MessageProperty = messageProperty,
            EntityProperty = keyProperty.Name,
            EntityName = string.Format("{0}_{1}", typeof(TEntity).Name, JoinRefIdx),
            ParameterIdx = ParameterIdx++
         });
         return this;
      }

      public ReferenceMapper<TMapper, TEntity, TMessage> Version(Expression<Func<TEntity, object>> versionExpression,
                                                            Expression<Func<TMessage, object>> messageExpression) {
         var versionProperty = ExpressionsHelper.GetMemberInfo(versionExpression) as PropertyInfo;
         var messageProperty = ExpressionsHelper.GetMemberInfo(messageExpression) as PropertyInfo;

         if (versionProperty == null) return this;
         if (messageProperty == null) return this;

         _isKeyed = true;
         _keyProperty = messageProperty;

         _parameters.Add(new ProjectionPart {
            MessageProperty = messageProperty,
            EntityProperty = versionProperty.Name,
            EntityName = string.Format("{0}_{1}", typeof(TEntity).Name, JoinRefIdx),
            ParameterIdx = ParameterIdx++
         });
         return this;
      }

      public ParameterMapper<ReferenceMapper<TMapper, TEntity, TMessage>, TEntity, TMessage>
         Projection(Expression<Func<TMessage, object>> messageExpression)
      {
         var messageProperty = ExpressionsHelper.GetMemberInfo(messageExpression) as PropertyInfo;
         var mapper = new ParameterMapper<ReferenceMapper<TMapper, TEntity, TMessage>, TEntity, TMessage>(this, messageProperty);
         return mapper;
      }

      public ExpressionMapper<ReferenceMapper<TMapper, TEntity, TMessage>, TEntity> Criteria()
      {
         var entityName = typeof(TEntity).Name;
         var mapper = new ExpressionMapper<ReferenceMapper<TMapper, TEntity, TMessage>, TEntity>(this) 
            { EntityName = string.Format("{0}_{1}", entityName, EntityLevel) };

         Citerion.Add(mapper);
         return mapper;
      }

      public OrderingMapper<ReferenceMapper<TMapper, TEntity, TMessage>, TEntity> OrderBy() {
         var entityName = string.Format("{0}_{1}", typeof(TEntity).Name, 0);
         var mapper = new OrderingMapper<ReferenceMapper<TMapper, TEntity, TMessage>, TEntity>(this, entityName);

         Ordering.Add(mapper);
         return mapper;
      }

      public ReferenceMapper<ReferenceMapper<TMapper, TEntity, TMessage>, TRefEntity, TRefMessage>
         Reference<TRefEntity, TRefMessage>(Expression<Func<TEntity, object>> referencePath, Expression<Func<TMessage, object>> messageProperty)
      {
         var path = referencePath.Body.ToString();
         var entity = referencePath.Parameters[0].Type.Name.ToLower();
         var messageInfo = ExpressionsHelper.GetMemberInfo(messageProperty) as PropertyInfo;

         JoinRefIdx++;
         var mapper = new ReferenceMapper<ReferenceMapper<TMapper, TEntity, TMessage>, TRefEntity, TRefMessage>(this, messageInfo, entity, path) 
         { JoinRefIdx = JoinRefIdx, EntityLevel = JoinRefIdx };
         return mapper;
      }

      public ReferenceMapper<ReferenceMapper<TMapper, TEntity, TMessage>, TRefEntity, TRefMessage>
         Reference<TRefEntity, TRefMessage>(Expression<Func<TEntity, object>> referencePath)
      {
         var path = referencePath.Body.ToString();
         var entity = referencePath.Parameters[0].Type.Name.ToLower();

         JoinRefIdx++;
         var mapper = new ReferenceMapper<ReferenceMapper<TMapper, TEntity, TMessage>, TRefEntity, TRefMessage>(this, null, entity, path) 
         { JoinRefIdx = JoinRefIdx, EntityLevel = JoinRefIdx };
         return mapper;
      }

      public TMapper AddReference()
      {
         var part = ReferencePartFactory.GetReferencePart(_referenceType);
         part.ReferencePath = _referencePath;
         part.RootType = string.Format("{0}_{1}", _rootType, _projector.EntityLevel);
         part.RefEntityType = string.Format("{0}_{1}", typeof (TEntity).Name, EntityLevel);
         part.JoinType = _joinType;
         part.MessageType = typeof (TMessage);
         part.SubProjection = _subProjection;
         part.Parameters = _parameters;
         part.References = _references;
         part.IsKeyed = _isKeyed;
         part.KeyProperty = _keyProperty;

         _projector.AddReferencePart(part);
         return _projector;
      }

      public void AddParameterPart(ProjectionPart parameterPart)
      {
         _parameters.Add(parameterPart);
      }

      public void AddReferencePart(IReferencePart referencePart)
      {
         _references.Add(referencePart);
      }

      public IDictionary<string, object> CriteriaParameters
      {
         get { return _projector.CriteriaParameters; }
         set { _projector.CriteriaParameters = value; }
      }

   }

   public enum ReferenceType
   {
      CollectionPart,
      PropertyPart,
      TypePart
   }

   public enum ReferenceJoinType
   {
      inner, left, right, none
   }
}