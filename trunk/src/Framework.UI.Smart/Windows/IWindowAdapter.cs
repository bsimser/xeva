using System;
using System.ComponentModel;

namespace XF.UI.Smart
{
   public interface IWindowAdapter : IWindowController 
   {
      void InitializeUI(object ui);

      void ApplyOptions(IWindowOptions options);
   }
}