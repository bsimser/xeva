using System;
using System.Collections.Generic;
using System.Text;

namespace XEVA.Framework.UI.Smart
{

   [AttributeUsage(AttributeTargets.Method)]
   public class CommandAttribute : Attribute
   {
      private string _label;
      private bool _visible;
      private bool _enabled;

      public CommandAttribute(string label, bool enabled, bool visible)
      {
         _label = label;
         _visible = visible;
         _enabled = enabled;
      }

      public CommandAttribute()
      {
         _label = string.Empty;
         _visible = true;
         _enabled = true;
      }

      public string Label
      {
         get { return this._label; }
      }

      public bool Visible
      {
         get { return this._visible; }
      }

      public bool Enabled
      {
         get { return this._enabled; }
      }
   }
}
