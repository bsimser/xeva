using System;
using System.ComponentModel;
using XF.Validation;

namespace XF.Controls
{
   interface IPropertyControlAdapter
   {
      void SetSubject(INotifyPropertyChanged dto, string property);

      event EventHandler ValueChanged;

      void SetNotification(ValidationResult validationResult);
   }
}
