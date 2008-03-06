using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace XF.Services
{
   public class ExceptionShield
   {
      private const string _eventSource = "XEVA";
      private const string _eventLogName = "XEVA";

      private static object[] _parameters;
      private static Exception _exception;
      private static ExceptionMessage _exceptionMessage;

      public ExceptionShield(string serviceKey, string methodKey, object[] parameters, Exception exception, string userFullName)
      {
         _parameters = parameters;
         _exception = exception;

         _exceptionMessage = new ExceptionMessage();
         _exceptionMessage.ServiceKey = serviceKey;
         _exceptionMessage.MethodKey = methodKey;
         _exceptionMessage.UserFullName = userFullName;
         _exceptionMessage.ExceptionsTime = DateTime.Now.ToString();
         _exceptionMessage.ExceptionMessages = GetExceptionMessages(exception);
         _exceptionMessage.StackTrace = exception.StackTrace;
         _exceptionMessage.ExceptionType = exception.GetType().Name;
      }

      public static Exception Exception
      {
         get { return _exception; }
      }

      public static ExceptionMessage ExceptionMessage
      {
         get { return _exceptionMessage; }
      }

      public static object[] Parameters
      {
         get { return _parameters; }
      }

      public void Log()
      {
         EventLog eventLog = new EventLog();
         if (!EventLog.SourceExists(_eventSource))
         {
            EventSourceCreationData srcData = new EventSourceCreationData(_eventSource, _eventLogName);
            EventLog.CreateEventSource(srcData);
         }
         eventLog.Source = _eventSource;
         eventLog.Log = _eventLogName;
         eventLog.WriteEntry(FormattedMessage(), EventLogEntryType.Error);
      }

      private string FormattedMessage()
      {
         string result;

         StringBuilder sb = new StringBuilder();
         sb.AppendLine(string.Format("Service Key: {0}", _exceptionMessage.ServiceKey));
         sb.AppendLine(string.Format("Method Key: {0}", _exceptionMessage.MethodKey));
         sb.AppendLine(string.Format("User: {0}", _exceptionMessage.UserFullName));
         sb.AppendLine(string.Format("Exception's Time: {0}", _exceptionMessage.ExceptionsTime));
         sb.AppendLine(string.Format("Exception Type: {0}", _exceptionMessage.ExceptionType));
         for (int prmIndex = 0; prmIndex < _parameters.Length ; prmIndex++)
         {
            sb.AppendLine(string.Format("Parameter {0} Value: {1}", prmIndex + 1, _parameters[prmIndex]));
         }
         for (int msgIndex = 0; msgIndex < _exceptionMessage.ExceptionMessages.Count; msgIndex++)
         {
            sb.AppendLine(string.Format("Exception {0} Message: {1}", msgIndex + 1, _exceptionMessage.ExceptionMessages[msgIndex]));
         }
         sb.AppendLine(string.Format("Stack Trace: {0}", _exceptionMessage.StackTrace));
         result = sb.ToString();

         return result;
      }

      private List<string> GetExceptionMessages(Exception exception)
      {
         List<string> result = new List<string>();

         result.Add(exception.Message);
         Exception innerException = null;
         if (exception is PreFilterProcessingException)
            innerException = ((PreFilterProcessingException)exception).FilterException;
         else if (exception is PostFilterProcessingException)
            innerException = ((PostFilterProcessingException)exception).FilterException;
         else innerException = exception.InnerException;
          
         while (innerException != null)
         {
            result.Add(innerException.Message);
            innerException = innerException.InnerException;
         }
         return result;
      }
   }
}