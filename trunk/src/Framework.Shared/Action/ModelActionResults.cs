using System;
using System.Runtime.Serialization;

namespace XF {
   [Serializable]
   public class ModelActionResults : IActionResults {

      public ModelActionResults() { }

      public ModelActionResults(bool succeeded, string message) {
         ResultCode = succeeded ? ActionResultCode.Success : ActionResultCode.Failure;
         Message = message;
      }

      public ActionResultCode ResultCode { get; set; }
      public string Message { get; set; }
      public string ErrorContent { get; set; }
      public object Data { get; set; }

      public static IActionResults NullInputResult {
         get { return new ModelActionResults { ResultCode = ActionResultCode.Failure, Message = "Action aborted null input" }; }
      }

      public static IActionResults InvalidEntityID {
         get {
            return new ModelActionResults { ResultCode = ActionResultCode.Failure, Message = "The entityID provided was invalid" };
         }
      }
   }

   public enum ActionResultCode { Success, Failure }

   public class ActionFailueException : Exception {

      public ActionFailueException(string message)
         : this(message, null) {

      }

      public ActionFailueException(string message, Exception exception)
         : base(message, exception) {
      }
   }
}