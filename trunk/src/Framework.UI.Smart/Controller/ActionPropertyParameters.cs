using System.Collections.Generic;
using System.Reflection;

namespace XF.UI.Smart
{
   public class ActionPropertyParameters : List<ActionPropertyParameters>
   {
      public PropertyInfo Output { get; set; }
      public PropertyInfo Input { get; set; }
      public object DefaultValue { get; set; }
      public PropertyInfo ListOfValues { get; set; }
      public bool IsPassthrough { get; set; }
   }
}