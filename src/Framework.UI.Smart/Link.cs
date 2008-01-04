using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace XEVA.Framework.UI.Smart
{
   public class Link
   {
      private byte[] _icon;
      private string _caption;
      private string _key;
      private IDictionary<string, object> _request;

      public static Link To(string key, string caption)
      {
         Link result = new Link();
         result.Icon = null;
         result.Key = key;
         result.Caption = caption; 
         return result;
      }

      public static Link To(string key, string caption, IDictionary<string, object> request)
      {
         Link result = To(key, caption);
         result._request = request;
         return result;
      }

      public byte[] Icon
      {
         get { return _icon; }
         set { _icon = value; }
      }

      public string Caption
      {
         get { return _caption; }
         set { _caption = value; }
      }

      public string Key
      {
         get { return _key; }
         set { _key = value; }
      }

      public IDictionary<string, object> Request
      {
         get { return _request; }
         set { _request = value; }
      }


   }
}
