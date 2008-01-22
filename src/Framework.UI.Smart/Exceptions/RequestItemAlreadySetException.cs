using System;

namespace XF.UI.Smart
{
   public class RequestItemAlreadySetException : Exception
   {
      private readonly string _key;

      public RequestItemAlreadySetException(string key)
         : base("An item with the key '{0}' already exists in the request")
      {
         _key = key;
      }

      public string Key
      {
         get { return _key; }
      }
   }
}