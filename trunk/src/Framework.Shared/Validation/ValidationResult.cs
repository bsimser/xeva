using System;
using System.Collections.Generic;

namespace XEVA.Framework.Validation
{
   public class ValidationResult
   {
      private List<ValidationMessage> _messages = new List<ValidationMessage>();

      public virtual bool IsValid
      {
         get { return _messages.Count == 0; }
      }

      public virtual IList<ValidationMessage> Messages
      {
         get { return this._messages; }
      }

      public virtual string DetailedErrorMessage()
      {
         string result = String.Empty;

         foreach (ValidationMessage message in _messages)
         {
            result += message.Property + ": " + message.Message + "\r\n";
         }

         return result;
      }

      public virtual void AddMessage(ValidationMessage message)
      {
         if (!_messages.Contains(message))
            _messages.Add(message);
      }

      public virtual void AddMessage(string property, string message)
      {
         ValidationMessage validationMessage = new ValidationMessage(property, message);
         this.AddMessage(validationMessage);
      }
   }
}