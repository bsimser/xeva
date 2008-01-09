using System;

namespace XEVA.Framework.UI.Smart 
{
   public class WindowManager : IWindowManager
   {
      private readonly IWindowAdapter _windowAdapter;

      public WindowManager(IWindowAdapter windowAdapter)
      {
         _windowAdapter = windowAdapter;
      }

      public IWindow Create()
      {
         return _windowAdapter.NewWindow();
      }
   }
}