using System;

namespace XF.Services
{
   public class XMLMessageSerializer : IXMLMessageSerializer
   {
      private readonly ISerializeAdapter _serializer;
      private readonly IStreamAdapter _stream;

      public XMLMessageSerializer(IStreamAdapter streamAdapter, ISerializeAdapter serializeAdapter)
      {
         _stream = streamAdapter;
         _serializer = serializeAdapter;
      }

      public void Initialize(Type argumentType)
      {
         _serializer.Initialize(argumentType);
         _stream.Initialize();
      }

      public string Serialize(object argument)
      {
         _serializer.Serialize(_stream, argument);
         return _stream.ReadString();
      }

      public object Deserialize(string xmlArgument)
      {
         _stream.WriteString(xmlArgument);
         return _serializer.Deserialize(_stream);
      }

      public void Dispose()
      {
         if (_stream.IsOpen) _stream.Close();
      }

   }
}