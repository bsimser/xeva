using System;

namespace XEVA.Framework.UI.Smart
{
   public class DefaultLayoutResolver : ILayoutResolver
   {
      private readonly ILayoutLocator _locator;

      public DefaultLayoutResolver(ILayoutLocator locator)
      {
         this._locator = locator;
      }

      public ILayout GetLayout()
      {
         return _locator.FindLayout(ControllerBuilder.DEFAULT_LAYOUT_KEY);
      }
   }
}