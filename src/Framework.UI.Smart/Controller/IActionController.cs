using System;

namespace XF.UI.Smart
{
   public interface IActionController
   {
      event EventHandler ActionComplete;
      event EventHandler ActionCanceled;
      IActionResults ActionResults { get; }
      void PerformAction();
      void CancelAction();
      void Activate();
      void Activate(IRequest request);
      void Finish();
   }
}