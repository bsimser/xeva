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
      private string _methodName;
      private Type _serviceType;
      private ITransport transport;
      private IList<IChannelFilter> _filters;

      public ChannelIntercept(ITransport transport)
      {
         this.transport = transport;
      }

      public IList<IChannelFilter> FiltersList
      {
         get { return _filters; }
         set { _filters = value; }
      }

      public event EventHandler TransportFailed;

      public ITransport Transport
      {
         get { return transport; }
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

      public string MethodName
      {
         get { return _methodName; }
         set { _methodName = value; }
      }

      public void Intercept(IInvocation invocation)
      {
         RequestState requestState = new RequestState();
         ResponseState responseState = new ResponseState();

         requestState.Invocation = invocation;
         requestState.ServiceName = ServiceName;
         ProcessPreFilters(requestState, responseState);

         try
         {
            responseState.Content = transport.SendChannelRequest(requestState.Content);
         }
         catch (Exception e)
         {
            RaiseTransportFailure();
         }

         ProcessPostFilters(requestState, responseState);
      }

      private void ProcessPreFilters(RequestState requestState, ResponseState responseState)
      {
         foreach (IChannelFilter filter in _filters)
         {
            if (filter.FilterType == "pre")
            {
               filter.RequestState = requestState;
               filter.ResponseState = responseState;
               filter.Process();
            }
         }
      }

      private void ProcessPostFilters(RequestState requestState, ResponseState responseState)
      {
         foreach (IChannelFilter filter in _filters)
         {
            if (filter.FilterType == "post")
            {
               filter.RequestState = requestState;
               filter.ResponseState = responseState;
               filter.Process();
            }
         }
      }

      private void RaiseTransportFailure()
      {
         if (TransportFailed != null)
            TransportFailed(this, new EventArgs());
      }

   }
}