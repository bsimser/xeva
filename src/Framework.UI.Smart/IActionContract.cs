using System;

namespace XF.UI.Smart {
   public interface IActionContract {
      void AddUIToTab(string tabKey, object ui);
      void AddTabControl(string key, string caption, Action tabAction);
      void DisableTabControl(string key);
      void EnableTabControl(string key);
      void MenuToolVisability(string toolKey, bool isVisable);
      void AddMenuControl(string toolName);
      void AddMenuControl(string toolName, bool isVisable);
      void AddSubMenuControl(string parentTool, string toolName, string toolCaption, bool b);
      void AddMenuButtonControl(string parentTool, string toolKey, string toolCaption, Action action);
      void AddMenuButtonControl(string parentTool, string toolKey, string toolCaption, Action action, bool isVisable);
      void StatusBarOpen(string text);
      void StatusBarClose();
      void StatusbarProgress(int progress);
   }
}