using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace XF.Controls
{
   [ToolboxItem(true)]
   public partial class PlaceHolderControl : XFControlBase
   {
      private Control _inputControl = null;
      private Color _validationColor;

      public PlaceHolderControl()
      {
         InitializeComponent();
         base.EditorControl = _placeHolderPanel;
         base.SetEditorControlPosition();
         DoubleBuffered = true;
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Control InputControl
      {
         get { return _inputControl; }
         set
         {
            if (_inputControl != null)
            {
               GotFocus -= delegate
                           {
                              _inputControl.Focus();
                           };
               _placeHolderPanel.Controls.Clear();
            }

            _inputControl = value;
            _inputControl.AutoSize = false;

            _placeHolderPanel.Controls.Add(value);
            _placeHolderPanel.Padding =
               new Padding(
                  _placeHolderPanel.Padding.Left, 
                  _placeHolderPanel.Padding.Top, 
                  _placeHolderPanel.Padding.Right, 
                  0);

            _inputControl.Dock = DockStyle.Fill;

            GotFocus += delegate
                        {
                           _inputControl.Focus();
                        };
         }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Color ValidationColor {
         get { return _validationColor; }
         set { _validationColor = value; }
      }

      public override void ShowError(string message)
      {
         _errorProvider.SetError(this, message);
         _inputControl.BackColor = _validationColor;
      }

      public override void ClearError()
      {
         _errorProvider.Clear();
         _inputControl.ResetBackColor();
      }

      private void OnEnter(object sender, System.EventArgs e)
      {
         _inputControl.Focus();
      }
   }
}