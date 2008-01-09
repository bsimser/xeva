using System;

namespace XEVA.Framework.UI.Smart 
{
   public class WindowManager : IWindowManager
   {
      private IWindowAdapter _windowAdapter;

      public WindowManager(IWindowAdapter windowAdapter)
      {
         _windowAdapter = windowAdapter;
      }

      public IWindow Create(IPresenter presenter)
      {
         if (presenter.UI == null) throw new NoUserInterfaceObjectException();
         IWindow window = _windowAdapter.NewWindow();
         window.InitializeUI(presenter.UI);
         return window;
      }
   }
}