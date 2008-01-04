using System.Collections.Generic;

namespace XEVA.Framework.UI.Smart
{
   public interface ILabelStore
   {
      string Location { set; }

      void PersistLabels(IDictionary<string, string> labels);

      IDictionary<string, string> ReadLabels();
   }
}