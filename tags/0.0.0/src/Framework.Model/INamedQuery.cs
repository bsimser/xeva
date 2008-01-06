using System.Collections.Generic;

namespace XEVA.Framework.Model
{
   public interface INamedQuery
   {
      string Name { get; }
      IDictionary<string, object> Parameters { get; }

      void SetParameter(string name, object value);
   }
}