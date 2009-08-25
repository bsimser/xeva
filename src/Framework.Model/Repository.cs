using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using XF.Model;

namespace XF.Model
{
   public abstract class Repository<TEntity> : IRepository<TEntity>
      where TEntity : Entity
   {
      public IOrderedQueryable<TEntity> All
      {
         get

         {
            GuardStoreNotNull();
            return Store.Query<TEntity>();
         }
      }

      public virtual TEntity FindBy(Guid id)
      {
         GuardStoreNotNull();
         TEntity result = Store.Load<TEntity>(id);
         return result;
      }

      public virtual IList<TEntity> FindByQuery(INamedQuery query)
      {
         GuardStoreNotNull();
         if (query == null)
            throw new ArgumentNullException("query");

         IList<TEntity> result;

         result = Store.Query<TEntity>(query);

         return result;
      }

      public virtual int GetScalar(INamedQuery query)
      {
         GuardStoreNotNull();
         if (query == null)
            throw new ArgumentNullException("query");

         int result = Store.GetScalar(query);
         return result;
      }

      public virtual void Save(TEntity entity)
      {
         GuardStoreNotNull();
         Store.Save(entity);
      }

      public virtual void Refresh(TEntity entity)
      {
         GuardStoreNotNull();
         Store.Refresh(entity);
      }

      public virtual void Delete(TEntity entity)
      {
         GuardStoreNotNull();
         Store.Delete(entity);
      }

      protected virtual IStore Store
      {
         get
         {
            return UnitOfWork.Store;
         }
      }

      protected virtual void GuardStoreNotNull()
      {
         if (this.Store == null)
            throw new InvalidOperationException("Store must be set!");
      }
   }
}