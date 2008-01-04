using System;
using System.ComponentModel;

namespace XEVA.Framework.Services
{
   public class SmartDTO : INotifyPropertyChanged
   {
      protected virtual void OnPropertyChanged(string propertyName)
      {
         if (this.PropertyChanged != null)
            this.PropertyChanged(null, new PropertyChangedEventArgs(propertyName));
      }

      public event PropertyChangedEventHandler PropertyChanged;
   }
}