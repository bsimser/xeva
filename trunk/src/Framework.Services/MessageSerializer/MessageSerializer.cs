using System;

namespace XEVA.Framework.Services
{
   public class MessageSerializer : IMessageSerializer
   {
      private readonly ISerializeAdapter _serializer;
      private readonly IStreamAdapter _stream;

      public MessageSerializer(IStreamAdapter streamAdapter, ISerializeAdapter serializeAdapter)
      {
         _stream = streamAdapter;
         _serializer = serializeAdapter;
      }

      public void Initialize(Type messageType)
      {
         _serializer.Initialize(messageType);
         _stream.Initialize();
      }

      public string Serialize(object message)
      {
         _serializer.Serialize(_stream, message);
         return _stream.Read();
      }

      public object Deserialize(string xmlDocument)
      {
         _stream.Write(xmlDocument);
         return _serializer.Deserialize(_stream);
      }

      public void Dispose()
      {
         if (_stream.IsOpen) _stream.Close();
      }

   }
}