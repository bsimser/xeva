using System;

namespace XEVA.Framework.UI.Smart
{
   [AttributeUsage(AttributeTargets.Property)]
   public class ParameterAttribute : Attribute
   {
      private readonly string _key;
      private readonly bool _isRequired = true;

      public ParameterAttribute(string key)
      {
         _key = key;
      }

      public ParameterAttribute(string key, bool isRequired) : this(key)
      {
         _key = key;
         _isRequired = isRequired;
      }

      public string Key
      {
         get { return this._key; }
      }

      public bool IsRequired
      {
         get { return this._isRequired; }
      }
   }
}