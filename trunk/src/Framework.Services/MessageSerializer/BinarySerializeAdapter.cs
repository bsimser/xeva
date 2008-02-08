using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace XF.Services
{
   public class BinarySerializeAdapter : ISerializeAdapter
   {
      private BinaryFormatter _serializer;

      public void Initialize(Type messageType)
      {
         _serializer = new BinaryFormatter();
      }

      public void Serialize(IStreamAdapter streamAdapter, object argument)
      {
         _serializer.Serialize(streamAdapter.Stream, argument);
      }

      public object Deserialize(IStreamAdapter streamAdapter)
      {
         return _serializer.Deserialize(streamAdapter.Stream);
      }
   }
}