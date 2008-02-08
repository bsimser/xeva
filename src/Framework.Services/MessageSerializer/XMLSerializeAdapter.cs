using System;
using System.Xml.Serialization;

namespace XF.Services
{
   public class XMLSerializeAdapter : ISerializeAdapter
   {
      private XmlSerializer _serializer;

      public void Initialize(Type messageType)
      {
         _serializer = new XmlSerializer(messageType);
      }

      public void Serialize(IStreamAdapter streamAdapter, object messageDTO)
      {
         _serializer.Serialize(streamAdapter.Stream, messageDTO);
      }

      public object Deserialize(IStreamAdapter streamAdapter)
      {
         return _serializer.Deserialize(streamAdapter.Stream);
      }
   }
}