using System.Drawing;
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
      private IWindowAdapter _windowAdapter;

      protected override void Before_each_spec()
      {
         _windowManager = Create<WindowManager>();
         _presenter = Mock<IPresenter>();
         _windowAdapter = Mock<IWindowAdapter>();
      }

      [Test]
      public void Create_a_new_window()
      {
         using (Record)
         {
            SetupResult.For(_presenter.UI).Return(new object());
            Expect.Call(Get<IWindowFactory>().Create()).Return(_windowAdapter);
         }
         using (Playback)
         {
            _windowManager.Create();
         }
      }

      [Test]
      public void Apply_window_options_to_the_window()
      {
         WindowOptions options = Mock<WindowOptions>();
         using (Record)
         {
            SetupResult.For(Get<IWindowFactory>().Create()).Return(_windowAdapter);
            options.ApplyOptionsTo(_windowAdapter);
         }
         using (Playback)
         {
            _windowManager.Create(options);
         }
      }

   }
}