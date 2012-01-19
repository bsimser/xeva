using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XF.UI.Smart
{
   public interface IViewCallbacks
   {
      Dictionary<string, IControl> Controls { get; }
      void RegisterControl(string property, IControl control);
      void RegisterControl<TMessage>(Expression<Func<TMessage, Object>> expression, IControl control);
      bool Validate(object target);
   }
}
