using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Castle.Core.Interceptor;

namespace XF.Services
{
   public class ChannelIntercept : IChannelIntercept
   {
      private string _serviceName;
      private Type _serviceType;
      private ITransport _transport;
      private IList<IChannelFilter> _preFilters;
      private IList<IChannelFilter> _postFilters;

      public ChannelIntercept(ITransport transport)
      {
         _transport = transport;
      }

      public IList<IChannelFilter> PreFilters
      {
         get { return _preFilters; }
         set { _preFilters = value; }
      }

      public IList<IChannelFilter> PostFilters
      {
         get { return _postFilters; }
         set { _postFilters = value; }
      }

      public string ServiceName
      {
         get { return _serviceName; }
         set { _serviceName = value; }
      }

      public Type ServiceType
      {
         get { return _serviceType; }
         set { _serviceType = value; }
      }

      public void Intercept(IInvocation invocation)
      {
         ChannelRequest channelRequest = new ChannelRequest();
         ChannelResponse channelResponse = new ChannelResponse();

         channelRequest.Invocation = invocation;
         channelRequest.ServiceName = ServiceName;
         ProcessPreFilters(channelRequest, channelResponse);
               
         try
         {
            channelResponse.Content = _transport.SendChannelRequest(channelRequest.Content);
         }
         catch (Exception)
         {
            throw;
         }

         ProcessPostFilters(channelRequest, channelResponse);
      }

      private void ProcessPreFilters(ChannelRequest channelRequest, ChannelResponse channelResponse)
      {
         foreach (IChannelFilter filter in _preFilters)
         {
            filter.ChannelRequest = channelRequest;
            filter.ChannelResponse = channelResponse;
            filter.Process();
         }
      }

      private void ProcessPostFilters(ChannelRequest channelRequest, ChannelResponse channelResponse)
      {
         foreach (IChannelFilter filter in _postFilters)
         {
            filter.ChannelRequest = channelRequest;
            filter.ChannelResponse = channelResponse;
            filter.Process();
         }
      }

   }
}