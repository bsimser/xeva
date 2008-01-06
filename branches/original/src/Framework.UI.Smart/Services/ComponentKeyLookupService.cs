using System;
using System.Collections.Generic;
using System.Text;

namespace XEVA.Framework.UI.Smart
{
   public class ComponentKeyLookupService
   {
      private Dictionary<string, string> _controllerKeyMap;
      private Dictionary<ComponentKeyType, string> _componentKeyPrefixMap;

      public ComponentKeyLookupService()
      {
         _controllerKeyMap = new Dictionary<string, string>();

         _componentKeyPrefixMap = new Dictionary<ComponentKeyType, string>();
         _componentKeyPrefixMap.Add(ComponentKeyType.Presenter, "Presenters.");
         _componentKeyPrefixMap.Add(ComponentKeyType.Command, "Commands.");
         _componentKeyPrefixMap.Add(ComponentKeyType.Layout, "Layouts.");
      }


      public virtual string StandardizeComponentKey(ComponentKeyType type, string componentKey)
      {
         if (componentKey.StartsWith(_componentKeyPrefixMap[type])) return componentKey;
         return string.Format("{0}{1}", _componentKeyPrefixMap[type], componentKey);
      }
   }

   public enum ComponentKeyType
   {
      Presenter,
      Command,
      Layout
   }
}
