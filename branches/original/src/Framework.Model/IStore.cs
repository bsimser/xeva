using System;
using System.Collections.Generic;
using XEVA.Framework.Model;

namespace XEVA.Framework.Model
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

      TEntity Load<TEntity>(Guid id) where TEntity : Entity, new();

      void Delete(object entity);

      void Save(object entity);

      void Refresh(object entity);

      IList<TEntity> Query<TEntity>(INamedQuery query) where TEntity : Entity, new();

      ITransaction CreateTransaction();
   }
}