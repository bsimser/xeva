using System;
using XEVA.Framework.Validation;

namespace XEVA.Framework.Validation
{
   public interface INotificationAware
   {
      void ResetNotification();

      ValidationResult ValidationResult { set; }
   }
}