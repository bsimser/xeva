using System;

namespace XF {
   [Serializable]
   public abstract class XFDTOBase : ITrackableDTO{
      public Guid ID { get; set; }
      public int Version { get; set; }
   }
}