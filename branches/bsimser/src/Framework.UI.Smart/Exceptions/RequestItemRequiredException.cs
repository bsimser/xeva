using System;

namespace XF.UI.Smart
{
   public class RequestItemRequiredException : Exception
   {
      private readonly string _key;

      public RequestItemRequiredException(string key) : 
         base(string.Format("Missing request item with the key '{0}'.", key))
      {
         _key = key;
      }

      public string Key
      {
         get { return _key; }
      }
   }
}