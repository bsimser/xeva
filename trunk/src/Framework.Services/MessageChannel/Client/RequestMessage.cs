using System;
using System.Xml.Serialization;

namespace XF.Services
{
   [Serializable]
   public class RequestMessage
   {
      public Guid SessionKey;
      public string ServiceKey;
      public string MethodKey;
      public MessageArgument[] MessageArgs;
   }
}