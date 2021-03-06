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
      private bool _closeConfirmation;
      private string _confirmationMessage;
      private string _confirmationCaption;
      private bool _openInSecondMonitor;

      public WindowOptions(bool modal, int width, int height, int left, int top, bool openInSecondMonitor)
      {
         _modal = modal;
         _width = width;
         _height = height;
         _top = top;
         _left = left;
         _openInSecondMonitor = openInSecondMonitor;
      }

      public WindowOptions() : this (false, 900, 700, 0, 0, false)
      {
      }

      public WindowOptions(bool modal, int width, int height)
         : this(modal, width, height, 0, 0, false)
      {
      }

      public WindowOptions(bool modal, int width, int height, bool openInSecondMonitor)
         : this(modal, width, height, 0, 0, openInSecondMonitor)
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

      public bool CloseConfirmation
      {
         get { return _closeConfirmation; }
         set { _closeConfirmation = value;}
      }

      public string ConfirmationMessage
      {
         get { return _confirmationMessage; }
         set { _confirmationMessage = value; }
      }

      public string ConfirmationCaption
      {
         get { return _confirmationCaption; }
         set { _confirmationCaption = value; }
      }

      public bool OpenInSecondMonitor
      {
         get { return _openInSecondMonitor; }
         set { _openInSecondMonitor = value; }
      }
   }
}