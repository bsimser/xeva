using System.Collections.Generic;
using System.ComponentModel;

namespace XEVA.Framework.UI.Smart
{
   public interface IBindingFilter<FIlteredType>
   {
      string FilterString { get; }
      List<PropertyDescriptor> Properties { get; }
      void Initialize(string filterString);
      bool IncludeItem(FIlteredType item);
   }
}