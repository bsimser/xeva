using System;

namespace XF.UI.Smart
{
   public interface IPresenter : IViewCallbacks
   {
      object UI { get; }
      
      string Key { get; set; }

      string Label { get; set; }

      IWindowController Window { get; }

      void Activate();

      void Activate(IRequest request);

      void Finish();

      WorkItemBuilder Queue { get; }

      event EventHandler<PresenterFinishedEventArgs> Finished;

      void DisplayIn(IWindowManager manager, IWindowOptions options);

      void DisplayIn(IWindowManager manager);
   }
}