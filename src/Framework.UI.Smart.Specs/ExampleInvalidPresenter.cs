using System;
using System.Collections.Generic;
using System.Text;

namespace XF.UI.Smart
{
   
   /// <summary>
   ///  This presenter is invalid because it does not implement <c>IExampleInvalidCallbacks</c>
   /// </summary>
   public class ExampleInvalidPresenter : 
      Presenter<IExampleInvalidView, IExampleInvalidCallbacks>
   {

   }

   public interface IExampleInvalidView : IView<IExampleInvalidCallbacks>
   {
   }

   public interface IExampleInvalidCallbacks : IViewCallbacks
   {
   }
}
