using System;

namespace XEVA.Framework.UI.Smart
{
   public class PresenterWithCallbackCommands : Presenter<SomeView>
   {
      private ILayout _layout;
      private bool _methodOneCalled = false;

      public PresenterWithCallbackCommands()
      {
         Key = "TestPresenter";
         Label = "My Test Presenter";
      }

      public override void CustomStart() {}

      [Command("Command One", false, true)]
      public void SomeMethod1()
      {
         _methodOneCalled = true;
      }

      public bool MethodOneCalled
      {
         get { return this._methodOneCalled; }
      }

      [Command("Command Two", true, false)]
      public void SomeMethod2() {}

      [Command("Delegate Bustigated", true, false)]
      public void SomeMethod2(bool test) {}
   }

   public class SomeView : IView
   {
      private object _ui = new object();

      public object UI
      {
         get { return this._ui; }
      }
   }
}