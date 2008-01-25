using System.Runtime.Serialization;

namespace XF.UI.Smart
{
   public class WindowOptions : IWindowOptions
   {
      private bool _modal;
      private int _width;
      private int _height;

      public WindowOptions()
      {
            
      }

      public WindowOptions(bool modal, int width, int height)
      {
         _modal = modal;
         _width = width;
         _height = height;
      }

      public bool Modal
      {
         get { return _modal; }
         set { _modal = value; }
      }

      public int Width
      {
         get { return _width; }
         set { _width = value; }
      }

      public int Height
      {
         get { return _height; }
         set { _height = value; }
      }

      public virtual void ApplyOptionsTo(IWindowAdapter windowAdapter)
      {
         windowAdapter.Width = _width;
         windowAdapter.Height = _height;
         windowAdapter.Modal = _modal;
      }
   }
}