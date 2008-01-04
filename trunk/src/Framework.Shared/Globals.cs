using System;
using System.Collections;
using System.Web;
// rhino commons credit

namespace XEVA.Framework
{
   public class Globals
   {
      private static ILocalData current = new LocalData();
      private static object LocalDataHashtableKey = new object();

      public static ILocalData Data
      {
         get { return current; }
      }

      private class LocalData : ILocalData
      {
         [ThreadStatic]
         private static Hashtable _threadHashtable;

         private static Hashtable LocalHashtable
         {
            get
            {
               if (HttpContext.Current == null)
                  return _threadHashtable ?? (_threadHashtable = new Hashtable());
               return GetWebHashtable();
            }
         }

         private static Hashtable GetWebHashtable()
         {
            Hashtable web_hashtable = HttpContext.Current.Items[LocalDataHashtableKey] as Hashtable;
            if (web_hashtable == null)
               HttpContext.Current.Items[LocalDataHashtableKey] = web_hashtable = new Hashtable();
            return web_hashtable;
         }

         public object this[object key]
         {
            get { return LocalHashtable[key]; }
            set
            {
               LocalHashtable[key] = value;
               ;
            }
         }

         public void Clear()
         {
            LocalHashtable.Clear();
         }
      }
   }
}