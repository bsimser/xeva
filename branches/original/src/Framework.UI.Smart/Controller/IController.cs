using System;
using System.Collections.Generic;

namespace XEVA.Framework.UI.Smart
{
   public interface IController
   {
      void Run();

      void Run(string commandKey);
   }
}