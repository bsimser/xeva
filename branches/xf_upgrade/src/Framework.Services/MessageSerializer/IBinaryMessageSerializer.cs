using System;

namespace XF.Services
{
   public interface IBinaryMessageSerializer : IDisposable
   {
      void Initialize(Type messageType);

      byte[] Serialize(object argument);

      object Deserialize(byte[] binaryArgument);
   }
}