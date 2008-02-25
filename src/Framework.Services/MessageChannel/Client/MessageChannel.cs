using System;

namespace XF.Services
{
   public class MessageChannel : IMessageChannel
   {
      private IChannelIntercept _channelIntercept;
      private IProxyGeneratorFactory _proxyFactory;

      public MessageChannel(IProxyGeneratorFactory proxyFactory)
      {
         _proxyFactory = proxyFactory;
      }

      public IChannelIntercept ChannelIntercept
      {
         get { return _channelIntercept; }
         set { _channelIntercept = value; }
      }

      public object GetChannelInterface()
      {
         return _proxyFactory.CreateInterfaceProxyWithoutTarget(_channelIntercept.ServiceType, _channelIntercept);
      }

      public virtual void InitializeChannel(string serviceName, Type serviceType)
      {
         _channelIntercept.ServiceName = serviceName;
         _channelIntercept.ServiceType = serviceType;
      }

   }
}