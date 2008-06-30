using System;
using XF.UI.Smart;

namespace BankTeller.UI.Smart.Presenters
{
   public delegate void LoginSuccessDelegate();

   public interface ILoginPresenter : IPresenter
   {
      event LoginSuccessDelegate LoginSuccess;
   }
}