using System;

namespace XF.Services
{
   public class MessageChannel : IMessageChannel
   {
      private IChannelIntercept _channelIntercept;

      public MessageChannel(IChannelIntercept channelIntercept)
      {
         _channelIntercept = channelIntercept;
      }

      public IChannelIntercept ChannelIntercept
      {
         get { return _channelIntercept; }
         set { _channelIntercept = value; }
      }

      public object GetChannelInterface()
      {
         return
            ProxyGeneratorFactory.Instance().CreateInterfaceProxyWithoutTarget(_channelIntercept.ServiceType,
                                                                               _channelIntercept);
      }

      public virtual void InitializeChannel(string serviceName, Type serviceType)
      {
         _channelIntercept.TransportFailed += OnTransportFailure;

         _channelIntercept.ServiceName = serviceName;
         _channelIntercept.ServiceType = serviceType;
      }

      private void OnTransportFailure(object sender, EventArgs e)
      {
         throw new Exception("Unhandled Transport Exception");
      }
   }
}