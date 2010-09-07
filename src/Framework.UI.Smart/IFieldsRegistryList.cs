using System.Collections.Generic;

namespace XF.UI.Smart
{
   public interface IFieldsRegistryList
   {
      List<IEditable> RegisteredControls { get; }
      List<IEditable> EditControls { get; }
      bool Validate();
   }
}