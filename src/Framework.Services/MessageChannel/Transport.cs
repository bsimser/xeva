using System;

namespace XF.Services
{
   public class Transport<TTransport> : ITransport
   {
      private TTransport _transport;

      public Transport()
      {
         _transport = Activator.CreateInstance<TTransport>();
      }

      public virtual byte[] SendChannelRequest(byte[] requestMessage)
      {
         object[] requestPrams = new object[1] { requestMessage };
         return (byte[])_transport.GetType().GetMethod("SendChannelRequest").Invoke(_transport, requestPrams);
      }

   }
}