using System;

namespace XF.Services
{
   public class PreFilterProcessingException : Exception
   {
      private readonly Exception _innerException;

      public PreFilterProcessingException(Exception innerException)
      {
         _innerException = innerException;
      }

      public Exception FilterException
      {
         get { return _innerException; }
      }
   }
}