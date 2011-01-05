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
      public bool Success { get { return ResultCode == XFResultCode.Success; } }
      public string Message { get; set; }
      public string ErrorContent { get; set; }
      public object Data { get; set; }

      public TData GetData<TData>() {
         return (TData)Data;
      }

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

      public static void ThrowNullInputs() {
         throw new ActionFailueException("Null inputs were detected");
      }
   }

   public class ActionFailueException : Exception {

      public ActionFailueException(string message)
         : this(message, null, null) {

      }

      public ActionFailueException(string message, IXFResults results)
         : this(message, results, null) {

      }

      public ActionFailueException(string message, IXFResults results, Exception exception)
         : base(message, exception) {
         Data.Add("results", results);
      }
   }
}