using System;

namespace XF
{
   [Serializable]
   public class ModelActionResults : IActionResults
   {
      public string ErrorCode { get; set; }
      public string Message { get; set; }
      public string ErrorContent { get; set; }
   }
}