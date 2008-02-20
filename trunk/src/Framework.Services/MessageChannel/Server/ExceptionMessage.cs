using System;
using System.Collections.Generic;

namespace XF.Services
{
   [Serializable]
   public class ExceptionMessage
   {
      public string ErrorLevel;
      public string ServiceKey;
      public string MethodKey;
      public string ExceptionType;
      public List<string> ExceptionMessages;
      public string UserFullName;
      public string ExceptionsTime;
      public string StackTrace;
   }
}