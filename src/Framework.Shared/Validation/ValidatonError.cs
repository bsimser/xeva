using System;

namespace XF.Validation
{
   [Serializable]
   public class ValidatonError
   {
      private string _property;
      private string _message;

      public ValidatonError()
      {
      }

      public ValidatonError(string property, string message)
      {
         _property = property;
         _message = message;
      }

      public string Property
      {
         get { return _property; }
         set { _property = value; }
      }

      public string Message
      {
         get { return _message; }
         set { _message = value; }
      }

      public override bool Equals(object obj)
      {
         if (this == obj) return true;

         ValidatonError message = obj as ValidatonError;
         if (message == null) return false;
         return (Equals(_property, message._property) && Equals(_message, message._message));
      }

      public override string ToString()
      {
         return string.Format("Property: {0}, Message: {1}", _property, _message);
      }
   }
}