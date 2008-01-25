using System;

namespace XF.UI.Smart
{
   public interface IPresenter : IViewCallbacks
   {
      void Start();

      void Start(IRequest request);

      void Finish();

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

   }
}