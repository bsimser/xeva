using XF.UI.Smart;

namespace BankTeller.UI.Smart.Presenters
{
   public interface ILoginView : IAsyncView<ILoginCallbacks>
   {
      string Username { get; }
      string Password { get; }
      void ShowError(string message);
   }
}