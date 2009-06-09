using System;
using System.Collections.Generic;
using System.Text;
using Specs_for_Presenter;

namespace XF.UI.Smart
{
   public class ExampleWidgetPresenter : 
      Presenter<IExampleWidgetView, IExampleWidgetCallbacks>, IExampleWidgetPresenter
   {
      private string _requestData = string.Empty;
      private object _ui = new object();

      protected override void OnHandleRequest(IRequest request)
      {
         _requestData = request.GetRequiredItem<string>("data", string.Empty);
         HandleRequestCallCount += 1;
      }

      protected override void OnFirstActivation()
      {
         FirstActivationCallCount += 1;
      }

      protected override void OnEveryActivation()
      {
         EveryActivationCallCount += 1;
      }

      protected override void OnFinish()
      {
         FinishCount += 1;
      }

      public int FirstActivationCallCount { get; private set; }

      public int HandleRequestCallCount { get; private set; }

      public int EveryActivationCallCount { get; private set; }

      public int FinishCount { get; private set; }

      public string RequestDataFromCustomStartup
      {
         get { return _requestData; }
      }

      public void SetNullWindowUI()
      {
         _ui = null;
      }

      public override object UI
      {
         get
         {
            return _ui;
         }
      }
   }

   public interface IExampleWidgetView : IView<IExampleWidgetCallbacks>
   {
   }

   public interface IExampleWidgetCallbacks : IViewCallbacks
   {
   }
}
