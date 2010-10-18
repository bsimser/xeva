using System;

namespace XF.Clone {
   public class EntityCloneAttribute : Attribute{

      public CopyMethod Method { get; set; }
   }
}