using System;
using System.Collections.Generic;
using XF.Model;

namespace XF.Model
{
   public interface IRepository<TEntity> where TEntity : Entity, new()
   {
      TEntity FindByID(Guid id);

      IList<TEntity> FindByQuery(INamedQuery query);

      void Save(TEntity entity);

      void Refresh(TEntity entity);

      void Delete(TEntity entity);
   }
}