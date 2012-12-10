using System.Collections.Generic;

namespace XF.Services
{
   public interface IServiceActivator
   {
      List<IServiceFilter> PreFilters { set; }
      List<IServiceFilter> PostFilters { set; }

      void Initialize(ServiceRequest request, ServiceResponse response);
      void ProcessPreFilters();
      void InvokeServiceMethod();
      void ProcessPostFilters();
      void Release();
   }
}