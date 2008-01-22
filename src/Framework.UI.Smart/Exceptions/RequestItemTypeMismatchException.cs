using System;

namespace XF.UI.Smart
{
   public class RequestItemTypeMismatchException : Exception
   {
      private readonly string _key;
      private readonly Type _expectedType;
      private readonly Type _foundType;

      public RequestItemTypeMismatchException(string key, Type expectedType, Type storedType)
         : base(string.Format("Item with key '{0}' expected a type of '{1}' but found a type of '{2}'.", key, expectedType.FullName, storedType.FullName))
      {
         _key = key;
         _foundType = storedType;
         _expectedType = expectedType;
      }

      public string Key
      {
         get { return _key; }
      }

      public Type Expected
      {
         get { return _expectedType; }
      }

      public Type Found
      {
         get { return _foundType; }
      }
   }
}