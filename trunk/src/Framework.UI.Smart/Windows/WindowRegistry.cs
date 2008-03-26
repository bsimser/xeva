
using System;
using System.Collections.Generic;

namespace XF.UI.Smart
{
   public class WindowRegistry : IWindowRegistry
   {
      private readonly Dictionary<Guid, IPresenter> _windows = new Dictionary<Guid, IPresenter>();

      public void RegisterWindow(Guid entityID, IPresenter presenter)
      {
         if(!_windows.ContainsKey(entityID))
            _windows.Add(entityID, presenter);
      }

      public bool WindowRegistered(Guid entityID)
      {
         return _windows.ContainsKey(entityID);
      }

      public IPresenter GetWindow(Guid entityID)
      {
         IPresenter result;

         if (_windows.ContainsKey(entityID))
            _windows.TryGetValue(entityID, out result);
         else
            throw new Exception("Window not registered");

         return result;
      }

      public void RemoveWindow(Guid entityID)
      {
         if(_windows.ContainsKey(entityID))
            _windows.Remove(entityID);
      }
   }
}
