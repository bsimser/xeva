using System;

namespace XEVA.Framework.UI.Smart
{
   public interface IPresenter : IControllerItem
   {
      void Start();

      void Finish();

      object UI { get; }

      object ParentForm { get; set; }

      IContext Context { get; set; }
   }
}