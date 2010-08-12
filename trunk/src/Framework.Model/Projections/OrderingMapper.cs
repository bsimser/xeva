using System;
using System.Linq.Expressions;
using System.Reflection;

namespace XF.Model {
   public class OrderingMapper<TMapper, TEntity> : IOrderingMapper where TMapper : IEntityMapper{
      private readonly TMapper _projector;
      private readonly string _entityName;
      private string _direction;
      private string _orderField;

      public OrderingMapper(TMapper projector, string entityName) {
         _projector = projector;
         _entityName = entityName;
      }

      public string OrderClause { get { return string.Format(" {0} {1},", _orderField, _direction); } }

      public OrderingMapper<TMapper, TEntity> Field(Expression<Func<TEntity, object>> entityColumn) {
         var property = ExpressionsHelper.GetMemberInfo(entityColumn) as PropertyInfo;
         if (property == null) return null;

         _orderField = string.Format("{0}.{1}", _entityName.ToLower(), property.Name);
         return this;
      }

      public TMapper Asc() {
        _direction = "asc";
         return _projector;
      }

      public TMapper Desc() {
         _direction = "desc";
         return _projector;
      }

   }
}