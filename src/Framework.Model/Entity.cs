using System;
using System.Collections.Generic;
using XF.Validation;

namespace XF.Model
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

      public virtual void Validate(ValidationResult validationResults) {}

      public virtual Entity TemplateCopy() {
         return TemplateCopyTool.GenerateTemplateCopy(GetType(), this, null, null);
      }

      public virtual Entity TemplateCopy(Type newType) {
         return TemplateCopyTool.GenerateTemplateCopy(newType, this, null, null);
      }

      public virtual Entity TemplateCopy(List<KeyValuePair<Action<object>, object>> copyActions) {
         return TemplateCopyTool.GenerateTemplateCopy(GetType(), this, null, copyActions);
      }

      public virtual Entity TemplateCopy(Entity parent, List<KeyValuePair<Action<object>, object>> copyActions) {
         return TemplateCopyTool.GenerateTemplateCopy(GetType(), this, parent, copyActions);
      }

   }
}