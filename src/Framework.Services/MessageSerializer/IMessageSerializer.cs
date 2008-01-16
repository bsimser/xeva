using System;

namespace XF.Services
{
   public interface IMessageSerializer : IDisposable
   {
      void Initialize(Type messageType);

      string Serialize(object message);

      object Deserialize(string xmlDocument);
   }
}