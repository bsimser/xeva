using System;
using System.Reflection;

namespace XF.Services
{
   public class BuildServiceMethodFilter : IServiceFilter
   {
      private ServiceRequest _serviceRequest;
      private ServiceResponse _serviceResponse;

      public ServiceRequest ServiceRequest
      {
         get { return _serviceRequest; }
         set { _serviceRequest = value; }
      }

      public ServiceResponse ServiceResponse
      {
         get { return _serviceResponse; }
         set { _serviceResponse = value; }
      }

      public void Process()
      {
         RequestMessage message = _serviceRequest.Message;
         _serviceRequest.ServiceMethod = ExtractSpecifiedMethodFromServiceInstance(_serviceRequest.Service, message);
         _serviceRequest.ServiceMethodArgs = LoadParameterValuesForServiceMethod(message, _serviceRequest.ServiceMethod);
      }

      private MethodInfo ExtractSpecifiedMethodFromServiceInstance(object service, RequestMessage message)
      {
         Type[] types = new Type[message.MessageArgs.Length];
         for (int idx = 0; idx < types.Length; idx++)
         {
            Type argType = Type.GetType(message.MessageArgs[idx].ArgumentType);
            types[idx] = argType;
         }

         return (types.Length > 0) ?
            service.GetType().GetMethod(message.MethodKey, types) :
            service.GetType().GetMethod(message.MethodKey);
      }

      private object[] LoadParameterValuesForServiceMethod(RequestMessage message, MethodInfo serviceMethod)
      {
         ParameterInfo[] parameters = serviceMethod.GetParameters();
         object[] result = new object[parameters.Length];

         for (int idx = 0; idx < parameters.Length; idx++)
         {
            result[idx] = message.MessageArgs[idx].Argument;
         }

         return result;
      }


   }
}