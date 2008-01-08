using System;

namespace XEVA.Framework.UI.Smart
{
   public class MissingRequiredRequestItemException : Exception
   {
      private readonly string _key;
      private readonly Type _type;

      public MissingRequiredRequestItemException(string key, Type type) : 
         base(string.Format("Missing request item with key {0} of type {1}.", key, type.Name))
      {
         _key = key;
         _type = type;
      }

      public string Key
      {
         get { return _key; }
      }

      public Type Type
      {
         get { return _type; }
      }
   }
}