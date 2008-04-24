using System;

namespace XF.Services
{
   public class BinaryMessageSerializer : IBinaryMessageSerializer
   {
      private readonly ISerializeAdapter _serializer;
      private readonly IStreamAdapter _stream;

      public BinaryMessageSerializer(IStreamAdapter streamAdapter, ISerializeAdapter serializeAdapter)
      {
         _stream = streamAdapter;
         _serializer = serializeAdapter;
      }

      public void Initialize(Type messageType)
      {
         _serializer.Initialize(messageType);
         _stream.Initialize();
      }

      public byte[] Serialize(object argument)
      {
         _serializer.Serialize(_stream, argument);
         return _stream.ReadBinary();
      }

      public object Deserialize(byte[] binaryArgument)
      {
         _stream.WriteBinary(binaryArgument);
         return _serializer.Deserialize(_stream);
      }

      public void Dispose()
      {
         if (_stream.IsOpen) _stream.Close();
      }
   }
}