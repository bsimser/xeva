using System;
using XEVA.Framework.Validation;

namespace XEVA.Framework.Model
{
   public abstract class Entity : IEntity, ISelfValidator
   {
      private Guid _id = Guid.Empty;
      private int _version = 0;

      public Entity() {}

      public virtual Guid ID
      {
         get { return _id; }
         set { _id = value; }
      }

      public virtual int Version
      {
         get { return _version; }
         set { _version = value; }
      }

      public override string ToString()
      {
         return this.ID.ToString();
      }

      public override bool Equals(object obj)
      {
         if (obj == null)
            return false;

         if (!this.GetType().Equals(obj.GetType()))
            return false;

         Entity entity = obj as Entity;
         if (entity == null)
            return false;

         return (this.ID == entity.ID) && (this.Version == entity.Version);
      }

      public virtual void Validate(Notification notifications) {}
   }
}