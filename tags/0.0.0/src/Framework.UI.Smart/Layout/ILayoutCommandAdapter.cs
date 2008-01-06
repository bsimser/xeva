using System;

namespace XEVA.Framework.UI.Smart
{
   public interface ILayoutCommandAdapter
   {
      void RegisterCommand(ICommand command);

      void ClearCommands();
   }
}