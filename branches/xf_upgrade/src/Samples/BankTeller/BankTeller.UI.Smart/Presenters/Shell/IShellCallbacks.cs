using XF.UI.Smart;

namespace BankTeller.UI.Smart.Presenters
{
   public interface IShellCallbacks : IViewCallbacks
   {
      void ToolSelected(string key);
   }
}