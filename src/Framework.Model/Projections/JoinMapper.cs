using System;
using System.Linq.Expressions;

namespace XF.Model {
   public interface IJoinMapper {
   }

   public class JoinMapper<TMapper, TEntity> : IJoinMapper where TMapper : IEntityMapper {
      private readonly TMapper _mapper;

      public JoinMapper(TMapper mapper) {
         _mapper = mapper;
      }

      public JoinMapper<TMapper, TEntity> With(Expression<Func<TEntity, object>> keyExpression) {
         return this;
      }

      public JoinMapper<TMapper, TEntity> Inner() {
         return this;
      }

      public JoinMapper<TMapper, TEntity> Left() {
         return this;
      }

      public JoinMapper<TMapper, TEntity> Right() {
         return this;
      }

      public TMapper Join() {
         return _mapper;
      }
   }
}