using System;

namespace XEVA.Framework.Validation
{
   [Serializable]
   public class NotificationMessage
   {
      private string _property;
      private string _message;

      public NotificationMessage() {}

      public NotificationMessage(string property, string message)
      {
         this._property = property;
         this._message = message;
      }

      public string Property
      {
         get { return this._property; }
         set { this._property = value; }
      }

      public string Message
      {
         get { return this._message; }
         set { this._message = value; }
      }

      public override bool Equals(object obj)
      {
         if (this == obj) return true;

         NotificationMessage message = obj as NotificationMessage;
         if (message == null) return false;
         return (Equals(_property, message._property) && Equals(_message, message._message));
      }

      public override string ToString()
      {
         return string.Format("Property: {0}, Message: {1}", _property, _message);
      }
   }
}