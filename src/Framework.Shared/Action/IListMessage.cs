using System;

namespace XF
{
   public interface IListMessage
   {
      string Name { get; set; }
      Guid ID { get; set; }
      string StatusColor { get; set; }
   }
}