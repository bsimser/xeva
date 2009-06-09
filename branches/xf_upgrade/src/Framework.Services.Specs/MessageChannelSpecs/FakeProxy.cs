namespace XF.Services
{
   public class FakeProxy : IFakeProxy
   {
      private IUserAccount _userAccount;

      public IUserAccount UserAccount
      {
         get { return _userAccount; }
         set { _userAccount = value; }
      }

      public string ProxyMethod(string agr)
      {
         return agr;
      }
   }
}