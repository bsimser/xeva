using NUnit.Framework;
using Rhino.Mocks;
using XEVA.Framework.Specs;
using XEVA.Framework.UI.Smart;

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
            _windowManager.Create(_presenter);
         }
      }

      [Test]
      public void Insert_the_presenter_UI_into_the_new_window()
      {
         object ui = new object();
         using (Record)
         {
            SetupResult.For(Get<IWindowAdapter>().NewWindow()).Return(_window);
            SetupResult.For(_presenter.UI).Return(ui);
            _window.InitializeUI(ui);
         }
         using (Playback)
         {
            _windowManager.Create(_presenter);
         }
      }

      [Test, ExpectedException(typeof(NoUserInterfaceObjectException))]
      public void Throw_an_error_if_the_presenter_UI_is_null()
      {
         using (Record)
         {
            SetupResult.For(Get<IWindowAdapter>().NewWindow()).Return(_window);
            Expect.Call(_presenter.UI).Return(null);
         }
         using (Playback)
         {
            _windowManager.Create(_presenter);
         }
      }

   }
}