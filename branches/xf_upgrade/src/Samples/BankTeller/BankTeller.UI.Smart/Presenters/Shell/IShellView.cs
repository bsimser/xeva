using XF.UI.Smart;

namespace BankTeller.UI.Smart.Presenters
{
   public interface IShellView : IView<IShellCallbacks>
   {
      void RegisterTool(string key, string label);
   }
}