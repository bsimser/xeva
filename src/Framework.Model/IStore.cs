using System;
using System.Collections.Generic;
using System.Linq;
using XF.Model;

namespace XF.Model
{
   /// <summary>
   /// Abstract representation of storage, retrieval, caching, etc. of 
   /// all entity data. We have an NHibernate implementation, for example.
   /// </summary>
   public interface IStore
   {
      void Open();

      void Close();

      void Flush();

      void Clear();

      TEntity Load<TEntity>(Guid id) where TEntity : Entity;

      void Delete(object entity);

      void Save(object entity);

      void Refresh(object entity);

      IList<TEntity> Query<TEntity>(INamedQuery query) where TEntity : IEntity;
      
      IOrderedQueryable<TEntity> Query<TEntity>() where TEntity : IEntity;

      ITransaction CreateTransaction();
   }
}