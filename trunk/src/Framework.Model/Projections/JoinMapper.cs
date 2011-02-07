using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XF.Model {
   public interface IJoinMapper : IHaveCriteriaMapper {
   }

   public class JoinMapper<TMapper, TEntity> : IJoinMapper where TMapper : IEntityMapper {
      private readonly TMapper _mapper;
      private IExpressionMapper _withCriteria;
      private ReferenceJoinType _joinType;

      public JoinMapper(TMapper mapper) {
         _mapper = mapper;
      }

      public IDictionary<string, object> CriteriaParameters {
         get { return _mapper.CriteriaParameters; }
         set { _mapper.CriteriaParameters = value; }
      }

      public int EntityLevel { get; set; }

      public WithMapper<JoinMapper<TMapper, TEntity>, TEntity> With() {
         var entityName = typeof(TEntity).Name;
         var with = new WithMapper<JoinMapper<TMapper, TEntity>, TEntity>(this) { EntityName = string.Format("{0}_{1}", entityName, EntityLevel) };

         _withCriteria = with;
         return with;
      }

      public JoinMapper<TMapper, TEntity> Inner() {
         _joinType = ReferenceJoinType.inner;
         return this;
      }

      public JoinMapper<TMapper, TEntity> Left() {
         _joinType = ReferenceJoinType.left;
         return this;
      }

      public JoinMapper<TMapper, TEntity> Right() {
         _joinType = ReferenceJoinType.right;
         return this;
      }

      public TMapper AddJoin() {
         ((IReferenceMapper) _mapper).JoinDefinition = new JoinPart {WithCriteria = _withCriteria, JoinType = _joinType};
         return _mapper;
      }

   }
}