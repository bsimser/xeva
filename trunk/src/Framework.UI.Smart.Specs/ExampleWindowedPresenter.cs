using System;
using System.Collections.Generic;
using System.Text;
using Specs_for_Presenter;

namespace XEVA.Framework.UI.Smart
{
   public class ExampleWindowedPresenter : 
      Presenter<IExampleWindowedView, IExampleWindowedCallbacks>, 
      IExampleWindowedCallbacks
   {
   
   }

   public interface IExampleWindowedView : IWindowView<IExampleWindowedCallbacks>
   {
   }

   public interface IExampleWindowedCallbacks : IPresenterCallbacks
   {
   }
}
