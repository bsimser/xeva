using System;
using System.Collections.Generic;
using System.Text;

namespace XEVA.Framework.UI.Smart
{
   public class InternalResolver : ILayoutResolver 
   {
      private readonly ILayout _layout;

      public InternalResolver(ILayout layout)
      {
         _layout = layout;
      }

      public ILayout GetLayout()
      {
         return _layout;
      }
   }
}
