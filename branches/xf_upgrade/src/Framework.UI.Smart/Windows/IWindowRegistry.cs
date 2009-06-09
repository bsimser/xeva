using System;

namespace XF.UI.Smart
{
   public interface IWindowRegistry
   {
      void RegisterWindow(Guid entityID, IPresenter presenter);

      bool WindowRegistered(Guid entityID);

      IPresenter GetWindow(Guid entityID);

      void RemoveWindow(Guid entityID);
   }
}
