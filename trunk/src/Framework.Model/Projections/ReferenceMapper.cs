using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using XF;

namespace XF.Model
{
   public class ReferenceMapper<TMapper, TEntity, TMessage> : IProjector
      where TMapper : IProjector
   {
      private readonly TMapper _projector;
      private readonly ProjectionPart _parameters = new ProjectionPart();
      private readonly List<IReferencePart> _references = new List<IReferencePart>();
      private readonly ReferenceExpression _expressions = new ReferenceExpression();
      private readonly string _referencePath;
      private readonly string _rootType;
      private readonly PropertyInfo _subProjection;
      private ReferenceJoinType _joinType = ReferenceJoinType.none;
      private ReferenceType _referenceType = ReferenceType.PropertyPart;

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

      public ReferenceMapper<TMapper, TEntity, TMessage> PartType(ReferenceType type)
      {
         _referenceType = type;
         return this;
      }

      public ReferenceMapper<TMapper, TEntity, TMessage> JoinType(ReferenceJoinType type)
      {
         _joinType = type;
         return this;
      }

      public ParameterMapper<ReferenceMapper<TMapper, TEntity, TMessage>, TEntity, TMessage>
         Projection(Expression<Func<TMessage, object>> messageExpression)
      {
         var messageProperty = ExpressionsHelper.GetMemberInfo(messageExpression) as PropertyInfo;
         var mapper = new ParameterMapper<ReferenceMapper<TMapper, TEntity, TMessage>, TEntity, TMessage>(this, messageProperty);
         return mapper;
      }

      public ReferenceMapper<TMapper, TEntity, TMessage> Where(Expression<Func<TEntity, object>> entityExpression,
                                                          ReferenceExpressionOperator expressionOperator, object value)
      {
         var messageProperty = ExpressionsHelper.GetMemberInfo(entityExpression) as PropertyInfo;

         if (messageProperty == null) return this;

         _expressions.Add(new ReferenceExpression
         {
            PropertyName = messageProperty.Name,
            PropertyPath = string.Format("{0}.{1}", _rootType, _referencePath),
            EntityName = string.Format("{0}_{1}", typeof(TEntity).Name, EntityLevel),
            Operator = expressionOperator,
            Value = value
         });
         return this;
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
         part.Expressions = _expressions;

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