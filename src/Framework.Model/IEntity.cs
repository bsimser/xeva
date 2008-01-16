using System;

namespace XF.Model
{
   public interface IEntity
   {
      Guid ID { get; set; }
      int Version { get; set; }
   }
}