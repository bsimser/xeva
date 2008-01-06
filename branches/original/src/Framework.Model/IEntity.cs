using System;

namespace XEVA.Framework.Model
{
   public interface IEntity
   {
      Guid ID { get; set; }
      int Version { get; set; }
   }
}