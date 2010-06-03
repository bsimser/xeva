using System;

namespace XF {
   public interface ITrackableDTO {
      Guid ID { get; set; }
      int Version { get; set; }
   }
}