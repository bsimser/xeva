using System;

namespace XEVA.Framework.Services
{
   public static class MessageSerializerFactory
   {
      public static IMessageSerializer Create(Type messageType)
      {
         IMessageSerializer result = new MessageSerializer(new StreamAdapter(), new SerializeAdapter());
         result.Initialize(messageType);

         return result;
      }
   }
}