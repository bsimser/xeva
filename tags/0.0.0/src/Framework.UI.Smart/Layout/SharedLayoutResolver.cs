using System;
using System.Collections.Generic;
using System.Text;

namespace XEVA.Framework.UI.Smart
{
   public class SharedLayoutResolver : ILayoutResolver
   {
      private readonly ILayoutLocator _locator;
      private readonly string _layoutKey;

      public SharedLayoutResolver(ILayoutLocator locator, string layoutKey)
      {
         this._locator = locator;
         _layoutKey = layoutKey;
      }

      public ILayout GetLayout()
      {
         return this._locator.FindLayout(_layoutKey);
      }
   }
}
