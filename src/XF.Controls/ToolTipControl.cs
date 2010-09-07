using System.Windows.Forms;

namespace XF.Controls
{
   public partial class ToolTipControl : Form
   {
      private bool _pinToolTip = false;

      public ToolTipControl()
      {
         InitializeComponent();
         WireKeyboardGesture();
      }

      public bool IsPinned
      {
         get { return _pinToolTip; }
         set { _pinToolTip = value; }
      }

      private void WireKeyboardGesture()
      {
         KeyDown += OnKeyDown;
         KeyPreview = true;
      }

      private void OnKeyDown(object sender, KeyEventArgs e)
      {
         if(e.Control && e.KeyCode == Keys.P)
            _pinToolTip = true;

         if(e.KeyCode == Keys.Escape)
            Close();
      }

      public string ToolTip
      {
         set
         {
            _toolTip.Text = value;
            _toolTip.Select(0,0);
         }
      }

      private void OnToolLoad(object sender, System.EventArgs e)
      {
      }

   }
}