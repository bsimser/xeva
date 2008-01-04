using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace XEVA.Framework.UI.Smart
{
   public interface ICommand : IControllerItem
   {
      bool Enabled { get; set; }

      bool Visible { get; set; }

      CommandDelegate Callback { set; }

      event EventHandler EnabledChanged;

      event EventHandler VisibleChanged;

      event CancelEventHandler Executing;

      event EventHandler Executed;

      void Execute();

      void Show();

      void Hide();

      ICommand Parent { get; set; }

      IDictionary<string, ICommand> Children { get; }
   }
}