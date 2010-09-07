using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XF.UI.Smart
{
   public interface IFieldsRegistry<TMessage> : IFieldsRegistryList
   {
      FieldsRegistry<TMessage> Map(Expression<Func<TMessage, object>> expression, IEditable control);
      TMessage GetHydratedMessage();
   }
}