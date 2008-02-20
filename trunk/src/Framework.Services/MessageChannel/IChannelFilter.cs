using System;
using Castle.Core.Interceptor;

namespace XF.Services
{
   public interface IChannelFilter
   {
      RequestState RequestState { get; set; }
      ResponseState ResponseState { get; set; }
      void Process();
   }
}