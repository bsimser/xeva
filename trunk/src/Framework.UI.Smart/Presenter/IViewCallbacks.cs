using System;
using System.Linq.Expressions;
using XF.Controls;

namespace XF.UI.Smart
{
   public interface IViewCallbacks
   {
      void RegisterControl(string property, IControl control);
      void RegisterControl<TMessage>(Expression<Func<TMessage, Object>> expression, IControl control);
      bool Validate(object target);
   }
}
