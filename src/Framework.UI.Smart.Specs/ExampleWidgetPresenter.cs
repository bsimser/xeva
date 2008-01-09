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
      private int _initializeCount = 0;
      private string _requestData = string.Empty;
      private int _finishCount = 0;

      protected override void InitializeRequest(IRequest request)
      {
         _requestData = request.GetItem<string>("data", string.Empty, true);
         _initializeCount += 1;
      }

      public override void CustomStart()
      {
         _startCount += 1;
      }

      public override void CustomFinish()
      {
         _finishCount += 1;
      }

      public int StartCount
      {
         get { return _startCount; }
      }

      public int InitializeCount
      {
         get { return _initializeCount; }
      }

      public string RequestDataFromCustomStartup
      {
         get { return _requestData; }
      }

      public int FinishCount
      {
         get { return _finishCount; }
      }
   }

   public interface IExampleWidgetView : IView<IExampleWidgetCallbacks>
   {
   }

   public interface IExampleWidgetCallbacks : IViewCallbacks
   {
   }
}