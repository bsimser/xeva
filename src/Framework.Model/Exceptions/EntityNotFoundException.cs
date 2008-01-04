using System;
using System.Runtime.Serialization;

namespace XEVA.Framework.Model
{
   public class EntityNotFoundException : Exception
   {
      private Guid _suppliedEntityID;

      public EntityNotFoundException()
      {
      }

      public EntityNotFoundException(string message) : base(message)
      {
      }

      public EntityNotFoundException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      public EntityNotFoundException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }

      public Guid SuppliedEntityID
      {
         get { return _suppliedEntityID; }
         set { _suppliedEntityID = value; }
      }
   }
}