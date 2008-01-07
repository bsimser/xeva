using System.Collections.Generic;
using System.ComponentModel;

namespace XEVA.Framework.UI.Smart
{
   public interface IBindingFilter<T>
   {
      string FilterString { get; }
      List<PropertyDescriptor> Properties { get; }
      void Initialize(string filterString);
      bool IncludeItem(T item);
   }
}