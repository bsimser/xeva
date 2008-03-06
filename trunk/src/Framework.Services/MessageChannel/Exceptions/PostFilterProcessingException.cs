using System;

namespace XF.Services
{
   public class PostFilterProcessingException : Exception
   {
      private readonly Exception _innerException;

      public PostFilterProcessingException(Exception innerException)
      {
         _innerException = innerException;
      }

      public Exception FilterException
      {
         get { return _innerException; }
      }
    }
}