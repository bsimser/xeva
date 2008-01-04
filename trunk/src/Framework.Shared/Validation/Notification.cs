using System;
using System.Collections.Generic;

namespace XEVA.Framework.Validation
{
   public class Notification
   {
      private List<NotificationMessage> _messages = new List<NotificationMessage>();

      public virtual bool IsValid
      {
         get { return _messages.Count == 0; }
      }

      public virtual IList<NotificationMessage> Messages
      {
         get { return this._messages; }
      }

      public virtual string DetailedErrorMessage()
      {
         string result = String.Empty;

         foreach (NotificationMessage message in _messages)
         {
            result += message.Property + ": " + message.Message + "\r\n";
         }

         return result;
      }

      public virtual void AddMessage(NotificationMessage message)
      {
         if (!_messages.Contains(message))
            _messages.Add(message);
      }

      public virtual void AddMessage(string property, string message)
      {
         NotificationMessage notificationMessage = new NotificationMessage(property, message);
         this.AddMessage(notificationMessage);
      }
   }
}