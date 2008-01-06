using System;
using System.Collections.Generic;
using XEVA.Framework.Model;

namespace XEVA.Framework.Model
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