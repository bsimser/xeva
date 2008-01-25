
using NUnit.Framework;
using XF.Specs;
using XF.UI.Smart;

namespace Specs_for_WindowOptions
{
   [TestFixture]
   public class When_applying_options_to_a_window_adapter : Spec
   {
      private WindowOptions _options;
      private IWindowAdapter _adapter;
      
      protected override void Before_each_spec()
      {
         _options = new WindowOptions(true, 800, 600);
         _adapter = Mock<IWindowAdapter>();
      }

      [Test]
      public void Set_the_modal_property()
      {
         using (Record) _adapter.Modal = true;
         _options.ApplyOptionsTo(_adapter);
      }

      [Test]
      public void Set_the_width_property()
      {
         using (Record) _adapter.Width = 800;
         _options.ApplyOptionsTo(_adapter);
         
      }

      [Test]
      public void Set_the_height_property()
      {
         using (Record) _adapter.Height = 600;
         _options.ApplyOptionsTo(_adapter);         
      }
   }
}
