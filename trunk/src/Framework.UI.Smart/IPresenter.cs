using System;

namespace XEVA.Framework.UI.Smart
{
   public interface IPresenter : IPresenterCallbacks
   {
      void Start(IRequest request);

      void Finish();

      object UI { get; }

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
   }
}