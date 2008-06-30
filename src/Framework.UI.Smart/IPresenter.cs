
using System;

namespace XF.UI.Smart
{
   public interface IPresenter : IViewCallbacks
   {
      void Start();

      void Start(IRequest request);

      void ReInitialize(IRequest request);

      void Finish();

      event EventHandler<PresenterFinishedEventArgs> Finished;

      object UI { get; }

      void DisplayIn(IWindowAdapter windowAdapter);

      string Key
      {
         get;
         set;
      }

      string Label
      {
         get;
         set;
      }

      IWindowController Window
      {
         get;
      }

      void DisplayIn(IWindowManager manager, IWindowOptions options);
   }
}