using System;

namespace XF
{
   [AttributeUsage(AttributeTargets.Property)]
   public class ActionPropertyAttribute : Attribute
   {
      public string Label { get; set; }
      public EditableControl EditorType { get; set; }
   }

}