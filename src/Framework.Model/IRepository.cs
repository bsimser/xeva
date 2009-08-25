using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using XF.Model;

namespace XF.Model
{
   public interface IRepository<TEntity> where TEntity : IEntity
   {
      IOrderedQueryable<TEntity> All { get; }

      TEntity FindBy(Guid id);

      IList<TEntity> FindByQuery(INamedQuery query);

      void Save(TEntity entity);

      void Refresh(TEntity entity);

      void Delete(TEntity entity);
   }
}