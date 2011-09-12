using System;
using System.Collections.Generic;

namespace XF.Model
{
   public interface IEntity
   {
      Guid ID { get; set; }
      int Version { get; set; }
      Entity TemplateCopy();
      Entity TemplateCopy(List<KeyValuePair<Action<object>, object>> copyActions);
      Entity TemplateCopy(Entity parent, List<KeyValuePair<Action<object>, object>> copyActions);
      Entity TemplateCopy(Type newType);
   }
}