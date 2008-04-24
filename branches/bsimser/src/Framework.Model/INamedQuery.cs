using System.Collections.Generic;

namespace XF.Model
{
   public interface INamedQuery
   {
      string Name { get; }
      IDictionary<string, object> Parameters { get; }

      void SetParameter(string name, object value);
   }
}