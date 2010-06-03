using System.Collections.Generic;

namespace XF.Model {
   public class ProjectionResults<TMessage> : IXFResults {
      public XFResultCode ResultCode { get; set; }
      public string Message { get; set; }
      public string ErrorContent { get; set; }
      public object Data { get; set; }
      public List<TMessage> Projection { get; set; }
   }
}