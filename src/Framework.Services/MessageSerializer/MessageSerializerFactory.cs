using System;

namespace XF.Services
{
   public static class MessageSerializerFactory
   {
      public static IXMLMessageSerializer CreateXMLSerializer(Type messageType)
      {
         IXMLMessageSerializer result = new XMLMessageSerializer(new StreamAdapter(), new XMLSerializeAdapter());
         result.Initialize(messageType);

         return result;
      }

      public static IBinaryMessageSerializer CreateBinarySerializer(Type messageType)
      {
         IBinaryMessageSerializer result = new BinaryMessageSerializer(new StreamAdapter(), new BinarySerializeAdapter());
         result.Initialize(messageType);

         return result;
      }
   }
}