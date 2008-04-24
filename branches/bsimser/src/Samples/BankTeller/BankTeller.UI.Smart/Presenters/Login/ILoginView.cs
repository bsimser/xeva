using XF.UI.Smart;

namespace BankTeller.UI.Smart.Presenters
{
   public interface ILoginView : IView<ILoginCallbacks>
   {
      string Username { get; }
      string Password { get; }
      void ShowError(string message);
   }
}