using System;
using System.Collections.Generic;
using System.Text;

namespace XEVA.Framework.UI.Smart
{
   public interface ILayoutLocator
   {
      void SaveLayout(string layoutKey, ILayout layout);
      
      ILayout FindLayout(string layoutKey);
   }
}
