using System;
using System.Collections.Generic;
using System.Text;

namespace XEVA.Framework.UI.Smart
{
   public interface IControl
   {
      string LabelText { get; set; }

      object Value { get; set; }

      bool ReadOnly { get; set; }
   }
}
