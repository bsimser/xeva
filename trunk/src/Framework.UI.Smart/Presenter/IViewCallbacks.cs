using System;
using System.Linq.Expressions;

namespace XF.UI.Smart
{
   public interface IViewCallbacks
   {
      void RegisterControl(string property, IControl control);
      void RegisterControl<TMessage>(Expression<Func<TMessage, Object>> expression, IControl control);
   }
}
