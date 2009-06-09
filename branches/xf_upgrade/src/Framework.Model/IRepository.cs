using System;
using System.Collections.Generic;
using System.Linq;
using XF.Model;

namespace XF.Model
{
   public interface IRepository<TEntity> where TEntity : IEntity
   {
      IQueryable<TEntity> All { get; }

      TEntity FindBy(Guid id);

      IList<TEntity> FindByQuery(INamedQuery query);

      void Save(TEntity entity);

      void Refresh(TEntity entity);

      void Delete(TEntity entity);
   }
}