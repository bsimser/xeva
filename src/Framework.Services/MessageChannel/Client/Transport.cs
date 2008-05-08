using System;

namespace XF.Services
{
   public class Transport<TTransport> : ITransport
   {
      private TTransport _transport;

      public virtual byte[] SendChannelRequest(byte[] requestMessage)
      {
         if (_transport == null)
            _transport = Locator.Resolve<TTransport>();

         object[] requestPrams = new object[1] { requestMessage };
         return (byte[])_transport.GetType().GetMethod("SendChannelRequest").Invoke(_transport, requestPrams);
      }

   }
}