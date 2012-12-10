using System;
using System.Collections.Generic;

namespace XF.Services
{
   public class ServiceActivator : IServiceActivator
   {
      private ServiceRequest _serviceRequest;
      private ServiceResponse _serviceResponse;
      private List<IServiceFilter> _preFilters;
      private List<IServiceFilter> _postFilters;

      public List<IServiceFilter> PreFilters
      {
         set { _preFilters = value; }
      }

      public List<IServiceFilter> PostFilters
      {
         set { _postFilters = value; }
      }

      public void Initialize(ServiceRequest request, ServiceResponse response)
      {
         _serviceRequest = request;
         _serviceResponse = response;
      }

      public void Release() {
         Locator.Release(_serviceRequest.Service);
         Locator.Release(this);
      }

      public void ProcessPreFilters()
      {
         try
         {
            foreach (IServiceFilter filter in _preFilters)
            {
               filter.ServiceRequest = _serviceRequest;
               filter.ServiceResponse = _serviceResponse;
               filter.Process();
            }
         }
         catch (Exception ex)
         {
            _serviceResponse.ServiceException = new PreFilterProcessingException(ex);
         }
      }

      public void InvokeServiceMethod()
      {
         try
         {
            if (_serviceResponse.ServiceException == null)
            {
               if(_serviceRequest.ValidSession)
               {
                  _serviceResponse.Message.ResponseObject = 
                     _serviceRequest.ServiceMethod.Invoke(_serviceRequest.Service, _serviceRequest.ServiceMethodArgs);
                  _serviceResponse.Message.SessionTicket = _serviceRequest.Message.SessionTicket;
               }
               else
               {
                  throw new InvalidSessionException();
               }
            }
         }
         catch (Exception exception)
         {
            _serviceResponse.ServiceException = exception;
         }
      }

      public void ProcessPostFilters()
      {
         try
         {
            foreach (IServiceFilter filter in _postFilters)
            {
               filter.ServiceRequest = _serviceRequest;
               filter.ServiceResponse = _serviceResponse;
               filter.Process();
            }
         }
         catch (Exception ex)
         {
            _serviceResponse.ServiceException = new PostFilterProcessingException(ex);
         }
      }
   }
}