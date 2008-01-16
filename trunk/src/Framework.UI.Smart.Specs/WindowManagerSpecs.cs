using NUnit.Framework;
using Rhino.Mocks;
using XF.Specs;
using XF.UI.Smart;

namespace Specs_for_WindowManager
{
   [TestFixture]
   public class When_opening_a_new_window : Spec
   {
      private WindowManager _windowManager;
      private IPresenter _presenter;
      private IWindow _window;

      protected override void Before_each_spec()
      {
         _windowManager = Create<WindowManager>();
         _presenter = Mock<IPresenter>();
         _window = Mock<IWindow>();
      }

      [Test]
      public void Create_a_new_window()
      {
         using (Record)
         {
            SetupResult.For(_presenter.UI).Return(new object());
            Expect.Call(Get<IWindowAdapter>().NewWindow()).Return(_window);
         }
         using (Playback)
         {
            _windowManager.Create();
         }
      }

   }
}