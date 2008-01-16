using System.Collections.Generic;
using System.ComponentModel;

namespace XF.UI.Smart
{
   public interface IBindingFilter<T>
   {
      string FilterString { get; }
      List<PropertyDescriptor> Properties { get; }
      void Initialize(string filterString);
      bool IncludeItem(T item);
   }
}