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

      public ModelActionResults(XFResultCode resultCode, string message, object data) {
         ResultCode = resultCode;
         Message = message;
         Data = data;
      }

      public XFResultCode ResultCode { get; set; }
      public string Message { get; set; }
      public string ErrorContent { get; set; }
      public object Data { get; set; }

      public static IXFResults SuccessfulAction(string message) {
         return new ModelActionResults { ResultCode = XFResultCode.Success, Message = "Action completed normally" };
      }

      public static IXFResults InvalidStateTransition(string stateType) {
         return new ModelActionResults
                {ResultCode = XFResultCode.Failure, Message = string.Format("Unable to transition to specified state: {0}", stateType)};
      }

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