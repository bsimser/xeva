using System;

namespace XF.Services
{
   public interface IMessageChannel
   {
      IChannelIntercept ChannelIntercept { get; }

      object GetChannelInterface();
      void InitializeChannel(string serviceName, Type serviceType);
   }
}