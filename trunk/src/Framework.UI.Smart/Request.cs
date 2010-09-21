using System;
using System.Collections.Generic;
using XF.UI.Smart;

namespace XF.UI.Smart {
   public class Request : IRequest {
      private readonly Dictionary<string, object> _items
         = new Dictionary<string, object>();

      public Request() {

      }

      public bool IsNull {
         get { return false; }
      }

      public T GetRequiredItem<T>(string key, T empty) {
         bool keyFound = _items.ContainsKey(key);
         if (!keyFound)
            throw new RequestItemRequiredException(key);
         TypeMatchGuard(key, typeof(T));

         return (T)_items[key];
      }

      public T GetRequiredItem<T>(T empty) {
         string key = typeof(T).FullName;
         return GetRequiredItem<T>(key, empty);
      }

      public T GetOptionalItem<T>(string key, T defaultValue) {
         bool keyFound = _items.ContainsKey(key);
         if (!keyFound) return defaultValue;
         TypeMatchGuard(key, typeof(T));
         return (T)_items[key];
      }

      public T GetOptionalItem<T>(T defaultValue) {
         string key = typeof(T).FullName;
         return GetOptionalItem<T>(key, defaultValue);
      }

      private void TypeMatchGuard(string key, Type expected) {
         Type storedType = _items[key].GetType();

         if (storedType.Equals(expected) ||
             expected.IsAssignableFrom(storedType)) return;

         throw new RequestItemTypeMismatchException(key, expected, storedType);
      }

      public void SetItem(string key, object value) {
         if (_items.ContainsKey(key))
            throw new RequestItemAlreadySetException(key);
         _items.Add(key, value);
      }

      public Request AddItem(string key, object value) {
         if (_items.ContainsKey(key)) return this;
         _items.Add(key, value);

         return this;
      }

      public void SetItem(object value) {
         string key = value.GetType().FullName;
         SetItem(key, value);
      }
   }
}