using System;
using System.Xml.Serialization;

namespace XF.Services
{
   [Serializable]
   public class RequestMessage
   {
      public bool IsAuthenticating;
      public Guid SessionTicket;
      public string ServiceKey;
      public string MethodKey;
      public MessageArgument[] MessageArgs;
   }
}