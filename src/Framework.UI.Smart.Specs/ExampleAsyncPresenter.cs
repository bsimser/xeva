using System;
using System.Collections.Generic;
using System.Text;
using Specs_for_Presenter;

namespace XF.UI.Smart
{
   public class ExampleAsyncPresenter : 
      Presenter<IExampleAsyncView, IExampleAsyncCallbacks>, IExampleAsyncCallbacks, IExampleAsyncPresenter
   {
      private int _startCount = 0;
      private int _initializeCount = 0;
      private string _requestData = string.Empty;
      private int _finishCount = 0;
      private object _ui = new object();

      protected override void OnHandleRequest(IRequest request)
      {
         _requestData = request.GetRequiredItem<string>("data", string.Empty);
         _initializeCount += 1;
      }

      protected override void OnFirstActivation()
      {
         _startCount += 1;
      }

      protected override void OnFinish()
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

   public interface IExampleAsyncPresenter : IPresenter
   {
   }

   public interface IExampleAsyncView : IAsyncView<IExampleAsyncCallbacks>
   {
   }

   public interface IExampleAsyncCallbacks : IViewCallbacks
   {
   }
}
