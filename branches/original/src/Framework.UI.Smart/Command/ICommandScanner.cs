using System.Collections.Generic;

namespace XEVA.Framework.UI.Smart
{
   public interface ICommandScanner
   {
      IList<ICommand> ScanForCommands(object presenter);
   }
}