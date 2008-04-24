using System;
using Castle.Core.Interceptor;

namespace XF.Services
{
   public interface IChannelFilter
   {
      ChannelRequest ChannelRequest { get; set; }
      ChannelResponse ChannelResponse { get; set; }
      void Process();
   }
}