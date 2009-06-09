using System;

namespace XF.UI.Smart
{
   public class ExampleWindowAdapter : IWindowAdapter 
   {
   
      public virtual void Show()
      {
      }

      public virtual void Hide()
      {
      }

      public virtual void Close()
      {
      }

      public virtual event EventHandler Closed;
      
      public virtual void ChangeCaption(string caption)
      {
      }

      public virtual void InitializeUI(object ui)
      {
      }

      public virtual void ApplyOptions(IWindowOptions options)
      {
      }
   }
}