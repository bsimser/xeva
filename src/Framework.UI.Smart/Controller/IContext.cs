using System;
using System.Collections.Generic;

namespace XEVA.Framework.UI.Smart
{
   public interface IContext
   {
      IDictionary<string, object> State { get; }

      object this[string key] { get; set; }

      event EventHandler StateChanged;

      bool Contains(string key);

      void Remove(string key);
   }
}