using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
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

      TEntity Load<TEntity>(Guid id) where TEntity : IEntity;

      void Delete(object entity);

      void Save(object entity);

      void Refresh(object entity);

      IList<TEntity> Query<TEntity>(INamedQuery query) where TEntity : IEntity;
      
      ITransaction CreateTransaction();

      int GetScalar(INamedQuery query);

      INHibernateQueryable<TEntity> Query<TEntity>() where TEntity : IEntity;

      ICriteria CreateCriteria<TEntity>();
   }
}