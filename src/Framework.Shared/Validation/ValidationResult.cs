using System;
using System.Collections.Generic;

namespace XEVA.Framework.Validation
{
   public class ValidationResult
   {
      private readonly List<ValidatonError> _errors = new List<ValidatonError>();

      public virtual bool IsValid
      {
         get { return _errors.Count == 0; }
      }

      public virtual IList<ValidatonError> Errors
      {
         get { return this._errors; }
      }

      public virtual string DetailedErrorMessage()
      {
         string result = String.Empty;

         foreach (ValidatonError message in _errors)
         {
            result += message.Property + ": " + message.Message + "\r\n";
         }

         return result;
      }

      public virtual void AddError(ValidatonError message)
      {
         if (!_errors.Contains(message))
            _errors.Add(message);
      }

      public virtual void AddError(string property, string message)
      {
         ValidatonError validationMessage = new ValidatonError(property, message);
         this.AddError(validationMessage);
      }
   }
}