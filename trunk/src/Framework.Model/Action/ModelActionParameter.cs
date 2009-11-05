using System;
using System.Reflection;

namespace XF
{
   public class ModelActionParameter
   {
      // Map by method
      public MethodInfo EntityMethod { get; set; }
      public object EntityPropertyValue { get; set; }

      // Map by property
      public PropertyInfo EntityProperty { get; set; }
      public PropertyInfo UpdateProperty { get; set; }
   }
}