using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XF.UI.Smart
{
   public class ExampleWindowManager : IWindowManager
   {
      public IWindowAdapter CreateWindowFor(IPresenter presenter)
      {
         return new ExampleWindowAdapter();
      }

      public IWindowOptions CreateDefaultWindowOptionsFor(IPresenter presenter)
      {
         return new WindowOptions(false, 800, 600); 
      }
   }
}
