using System.Collections;
using System.Collections.Generic;

namespace XEVA.Framework.UI.Smart
{
   public class Request : IDictionary<string, object>
   {
      private IDictionary<string, object> _internalDictionary;

      public Request(IDictionary<string, object> parameters)
      {
         _internalDictionary = parameters;
      }

      public static Request From(Link link)
      {
         Request request = new Request(link.Parameters);
         return request;
      }

      #region IDictionary<string,object> Members

      public bool ContainsKey(string key)
      {
         return _internalDictionary.ContainsKey(key);
      }

      public void Add(string key, object value)
      {
         _internalDictionary.Add(key, value);
      }

      public bool Remove(string key)
      {
         return _internalDictionary.Remove(key);
      }

      public bool TryGetValue(string key, out object value)
      {
         return _internalDictionary.TryGetValue(key, out value);
      }

      public object this[string key]
      {
         get { return _internalDictionary[key]; }
         set { _internalDictionary[key] = value; }
      }

      public ICollection<string> Keys
      {
         get { return _internalDictionary.Keys; }
      }

      public ICollection<object> Values
      {
         get { return _internalDictionary.Values; }
      }

      #endregion

      #region ICollection<KeyValuePair<string,object>> Members

      public void Add(KeyValuePair<string, object> item)
      {
         _internalDictionary.Add(item);
      }

      public void Clear()
      {
         _internalDictionary.Clear();
      }

      public bool Contains(KeyValuePair<string, object> item)
      {
         return _internalDictionary.Contains(item);
      }

      public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
      {
         _internalDictionary.CopyTo(array, arrayIndex);
      }

      public bool Remove(KeyValuePair<string, object> item)
      {
         return _internalDictionary.Remove(item);
      }

      public int Count
      {
         get { return _internalDictionary.Count; }
      }

      public bool IsReadOnly
      {
         get { return _internalDictionary.IsReadOnly; }
      }

      #endregion

      #region IEnumerable<KeyValuePair<string,object>> Members

      IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
      {
         return _internalDictionary.GetEnumerator();
      }

      #endregion

      #region IEnumerable Members

      public IEnumerator GetEnumerator()
      {
         return _internalDictionary.GetEnumerator();
      }

      #endregion
   }
}