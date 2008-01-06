using System;

namespace XEVA.Framework.UI.Smart
{
   public class ContextStateNotFoundException : Exception
   {
      public ContextStateNotFoundException(string key)
         : base(string.Format("ContextState with Key '{0} not found!", key)) {}
   }
}