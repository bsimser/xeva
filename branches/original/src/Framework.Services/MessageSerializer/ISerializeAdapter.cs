using System;

namespace XEVA.Framework.Services
{
   public interface ISerializeAdapter
   {
      void Initialize(Type messageType);

      void Serialize(IStreamAdapter streamAdapter, object messageDTO);

      object Deserialize(IStreamAdapter streamAdapter);
   }
}