using System;

namespace XF.Services
{
   public interface IXMLMessageSerializer : IDisposable
   {
      void Initialize(Type messageType);

      string Serialize(object message);

      object Deserialize(string xmlDocument);
   }
}