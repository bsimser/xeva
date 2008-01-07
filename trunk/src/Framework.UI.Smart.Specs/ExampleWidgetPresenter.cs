using System;
using System.Collections.Generic;
using System.Text;
using Specs_for_Presenter;

namespace XEVA.Framework.UI.Smart
{
   public class ExampleWidgetPresenter : 
      Presenter<IExampleWidgetView, IExampleWidgetCallbacks>, 
      IExampleWidgetCallbacks
   {
      private int _startCount = 0;
      private int _validateCount = 0;

      public override void CustomStart()
      {
         _startCount += 1;
      }

      protected override bool ValidateRequest(IRequest request)
      {
         string data = request.GetItem<string>("data", string.Empty);

         if (data == "test")
         {
            _validateCount += 1;
            return true;
         }
         else
         {
            throw new InvalidRequestException();
         }
      }

      public int StartCount
      {
         get { return _startCount; }
      }

      public int ValidateCount
      {
         get { return _validateCount; }
      }
   }

   public interface IExampleWidgetView : IView<IExampleWidgetCallbacks>
   {
   }

   public interface IExampleWidgetCallbacks : IPresenterCallbacks
   {
   }
}
