using System;
using System.Collections.Generic;
using System.Text;

namespace XEVA.Framework.UI.Smart
{
   public class Context : IContext
   {
      private readonly IDictionary<string, object> _state = new Dictionary<string, object>();

      public IDictionary<string, object> State
      {
         get { return _state; }
      }

      public object this[string key]
      {
         get
         {
            if (!Contains(key)) throw new ContextStateNotFoundException(key);
            return _state[key];
         }
         set 
         {
            if (!Contains(key))
            {
               _state.Add(key, value);
               OnStateChanged();
               return;
            }

            object originalValue = _state[key];
            object newValue = value;

            _state[key] = newValue;

            if (!originalValue.Equals(newValue))
               OnStateChanged(); 
         }
      }

      public bool Contains(string key)
      {
         return _state.ContainsKey(key);
      }

      public void Remove(string key)
      {
         if (_state.ContainsKey(key))
            _state.Remove(key);
      }

      public event EventHandler StateChanged;

      protected virtual void OnStateChanged()
      {
         if (this.StateChanged != null) this.StateChanged(this, null);
      }

   }

}
