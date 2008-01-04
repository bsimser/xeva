using System;

namespace XEVA.Framework.UI.Smart
{
   public interface ILayout
   {
      void DisplayPresenter(IPresenter presenter);

      void ShowLayout();

      void HideLayout();

      void RegisterLinkCommand(ICommand command);

      void RegisterChildCommand(ICommand command);

      void Close();

      event EventHandler Closed;

      void ClearCommands();
   }
}