using System;

namespace XF.Model  {
   public class ModelCopyAttribute : Attribute{
      public CopyMethod Method { get; set; }
      public Type GenerateAs { get; set; }

      public object Generate() {
         switch (GenerateAs.FullName) {
            case "System.Guid":
               return Guid.NewGuid();
            case "System.DateTime":
               return DateTime.Now;
            default:
               return null;
         }
      }
   }

   public enum CopyMethod {
      Copy, Template, Parent, Generate, None
   }
}