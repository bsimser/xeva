using System;
using System.Collections.Generic;
using XEVA.Framework.UI.Smart;

namespace XEVA.Framework.UI.Smart
{
   public class Request : IRequest
   {
      private readonly List<KeyValuePair<string, Type>> _index;
      private readonly Dictionary<KeyValuePair<string, Type>, object> _values;

      public Request()
      {
         _index = new List<KeyValuePair<string, Type>>();
         _values = new Dictionary<KeyValuePair<string, Type>, object>();
      }

      public virtual bool IsNull
      {
         get { return false; }
      }

      public T GetItem<T>(string key, T empty)
      {
         KeyValuePair<string, Type> indexKey = new KeyValuePair<string, Type>(key, typeof(T));
         if (!_index.Contains(indexKey)) return empty;
         T result = (T)_values[indexKey];
         return result;
      }

      public void SetItem<T>(string key, T value)
      {
         KeyValuePair<string, Type> indexKey = new KeyValuePair<string, Type>(key, typeof(T));
         if (!_index.Contains(indexKey)) _index.Add(indexKey);
         if (_values.ContainsKey(indexKey))
         {
            _values[indexKey] = value;
         }
         else
         {
            _values.Add(indexKey, value);
         }
      }
   }
}