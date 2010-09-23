using System;

namespace XF {
   [Serializable]
   public class LookupMessage {
      public Guid ID { get; set; }
      public string StatusColor { get; set; }
      public string Name { get; set; }
      public int DisplayOrder { get; set; }
   }
}