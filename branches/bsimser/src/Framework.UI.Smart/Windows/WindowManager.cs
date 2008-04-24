using System;

namespace XF.UI.Smart 
{
   public class WindowManager : IWindowManager
   {
      private readonly IWindowFactory _windowFactory;

      public WindowManager(IWindowFactory windowFactory)
      {
         _windowFactory = windowFactory;
      }

      public IWindowAdapter Create()
      {
         return _windowFactory.Create();
      }

      public IWindowAdapter Create(WindowOptions options)
      {
         IWindowAdapter result = Create();
         options.ApplyOptionsTo(result);
         return result;
      }
   }
}