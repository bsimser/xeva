using System.Collections.Generic;
using BankTeller.UI.Smart.Presenters;
using BankTeller.UI.Smart.Services;
using NUnit.Framework;
using Rhino.Mocks;
using XF.Specs;

namespace Specs_for_ShellPresenter
{
   [TestFixture]
   public class When_starting : Spec
   {
      private ShellPresenter _shellPresenter;

      protected override void Before_each_spec()
      {
         _shellPresenter = Create<ShellPresenter>();
      }

      [Test]
      public void Register_tools_in_the_shell()
      {
         var tools = new Dictionary<string, string> {{"one", "one"}, {"two", "two"}};
         SetupResult.For(Get<IProfileService>().GetShellTools()).Return(tools);
            
         using (Record)
         {
            Get<IShellView>().RegisterTool("one", "one");
            Get<IShellView>().RegisterTool("two", "two");
         }
         using (Playback)
         {
            _shellPresenter.Activate();
         }
      }

      [Test]
      public void Retrieve_tools_from_the_user_profile()
      {
         using (Record)
         {
            Expect.Call(Get<IProfileService>().GetShellTools()).Return(null);
         }
         using (Playback)
         {
            _shellPresenter.Activate();
         }
      }
   }
}