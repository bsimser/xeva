using System.Drawing;

namespace XF.UI.Smart
{
   public class WindowOptions : IWindowOptions
   {
      private int _height;
      private bool _modal;
      private int _width;
      private int _top;
      private int _left;

      public WindowOptions(bool modal, int width, int height, int left, int top)
      {
         _modal = modal;
         _width = width;
         _height = height;
         _top = top;
         _left = left;
      }

      public WindowOptions() : this (false, 800, 600, 0, 0)
      {
      }

      public WindowOptions(bool modal, int width, int height): this (modal, width, height, 0, 0)
      {
      }

      public Point Location
      {
         get
         {
            return new Point(_top, _left);
         }
         set
         {
            _left = value.X;
            _top = value.Y;
         }
      }

      public Size Size
      {
         get { return new Size(_width, _height); }
         set
         {
            _width = value.Width;
            _height = value.Height;
         }
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

      public int Left
      {
         get { return _left; }
         set { _left = value; }
      }

      public int Top
      {
         get { return _top; }
         set { _top = value; }
      }
   }
}