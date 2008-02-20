using System;

namespace XF.Services
{
   [Serializable]
   public class ResponseMessage
   {
      public object ResponseObject;
      public ExceptionMessage ExceptionMessage;
   }
}