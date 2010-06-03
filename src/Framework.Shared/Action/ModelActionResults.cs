using System;
using System.Runtime.Serialization;

namespace XF {
   [Serializable]
   public class ModelActionResults : IXFResults {

      public ModelActionResults() { }

      public ModelActionResults(bool succeeded, string message) {
         ResultCode = succeeded ? XFResultCode.Success : XFResultCode.Failure;
         Message = message;
      }

      public XFResultCode ResultCode { get; set; }
      public string Message { get; set; }
      public string ErrorContent { get; set; }
      public object Data { get; set; }

      public static IXFResults NullInputResult {
         get { return new ModelActionResults { ResultCode = XFResultCode.Failure, Message = "Action aborted null input" }; }
      }

      public static IXFResults InvalidEntityID {
         get {
            return new ModelActionResults { ResultCode = XFResultCode.Failure, Message = "The entityID provided was invalid" };
         }
      }
   }

   public class ActionFailueException : Exception {

      public ActionFailueException(string message)
         : this(message, null) {

      }

      public ActionFailueException(string message, Exception exception)
         : base(message, exception) {
      }
   }
}