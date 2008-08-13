using System.Threading;
using BankTeller.UI.Smart.Services;
using XF.UI.Smart;

namespace BankTeller.UI.Smart.Presenters
{
   public class LoginPresenter :
      Presenter<ILoginView, ILoginCallbacks>, ILoginPresenter,
      ILoginCallbacks
   {
      private readonly IAuthenticationService _authenticationService;
      private readonly ILabelLookup _labelLookup;
      private bool _success;
      private int count = 0;

      public LoginPresenter(IAuthenticationService authenticationService, ILabelLookup labelLookup)
      {
         _authenticationService = authenticationService;
         _labelLookup = labelLookup;
      }

      public virtual void Login()
      {
         View.ShowWaiting();

         Queue
            .Add(AttemptLogin)
            .Add(() => Thread.Sleep(1000)) // an illustration of the async stuff
            .Send(EvaluateSuccess);
      }

      private void AttemptLogin()
      {
         _success = _authenticationService.Authenticate(View.Username, View.Password);
      }

      private void EvaluateSuccess()
      {
         View.HideWaiting();

         if (_success)
         {
            OnLoginSuccess();
            Window.Close();
         }
         else
         {
            var errorMessage = _labelLookup.Find("INVALID_LOGIN");
            View.ShowError(errorMessage);
         }
      }

      public event LoginSuccessDelegate LoginSuccess;

      protected virtual void OnLoginSuccess()
      {
         if (LoginSuccess != null) LoginSuccess();
      }
   }
}