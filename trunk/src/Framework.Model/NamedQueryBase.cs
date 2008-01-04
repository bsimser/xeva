using System.Collections.Generic;

namespace XEVA.Framework.Model
{
   public class NamedQueryBase : INamedQuery
   {
      private string _name = "Unknown";
      private readonly IDictionary<string, object> _parameters = new Dictionary<string, object>();

      public string Name
      {
         get { return this._name; }
         protected set { this._name = value; }
      }

      public IDictionary<string, object> Parameters
      {
         get { return this._parameters; }
      }

      public void SetParameter(string name, object value)
      {
         _parameters[name] = value;
      }
   }
}