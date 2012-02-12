using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using XF.UI.Smart;

namespace XF.Controls {
   public partial class SelectorControl : UserControl, IControl {
      private const string NAME_PARAM = "Name";
      private const string ID_PARAM = "Id";
      private List<LookupMessage> _parameters = new List<LookupMessage>();
      private string _parameter = NAME_PARAM;

      public SelectorControl() {
         InitializeComponent();
      }

      public event EventHandler ParameterSelected;

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public string Label {
         get { return _label.Text; }
         set { _label.Text = value; }
      }

      public object Value {
         get { return null; }
         set { }
      }

      public bool ReadOnly {
         get { return false; }
         set { }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public int LabelWidth {
         get { return _label.Width; }
         set {
            _label.Width = value;
            SetEditorControlPosition();
         }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY),
       TypeConverter(typeof(ParameterConverter)),
       DefaultValue(NAME_PARAM)]
      public string Parameter {
         get { return _parameter; }
         set { _parameter = value; }
      }

      public List<LookupMessage> Parameters {
         set {
            _parameters = value;

            _parameterBindingSource.SuspendBinding();
            _parameterBindingSource.DataSource = _parameters;
            _parameterBindingSource.ResumeBinding();

            ButtonText = string.Empty;
         }
      }

      public string SelectedNameParameter {
         get { return SelectedRow != null ? SelectedRow.Cells[NAME_PARAM].Value.ToString() : string.Empty; }
      }

      public Guid SelectedIDParameter {
         get { return SelectedRow != null ? (Guid)SelectedRow.Cells[ID_PARAM].Value : Guid.Empty; }
      }

      public string ButtonText {
         set { _paramaterButton.Text = value; }
      }

      private UltraGridRow SelectedRow {
         get { return _parameterGrid.ActiveRow; }
      }

      private void OnClosedUp(object sender, EventArgs e) {
         SetButtonText();
         RaiseParameterSelected();
      }

      private void RaiseParameterSelected() {
         if (ParameterSelected != null &&
             SelectedRow != null)
            ParameterSelected(this, new EventArgs());
      }

      private void SetButtonText() {
         if (_parameterGrid.ActiveRow != null)
            _paramaterButton.Text = SelectedRow.Cells[NAME_PARAM].Value.ToString();
      }

      protected virtual void SetEditorControlPosition() {
         int left =
            _label.Width + _label.Left + (ControlConstants.LABEL_TO_CONTROL_SPACING);

         _paramaterButton.Left = left;

         _paramaterButton.Width = Width - left - (ControlConstants.CONTROL_PADDING);
      }

      private void OnResized(object sender, EventArgs e) {
         SetEditorControlPosition();
      }

      private void OnControlLoad(object sender, EventArgs e) {
         _parameterGrid.Width = _paramaterButton.Width;
      }

      private void OnButtonLostFocus(object sender, EventArgs e) {
         _paramaterButton.CloseUp();
      }

      private void OnParameterSelected(object sender, AfterSelectChangeEventArgs e) {
         if (_parameterGrid.Selected.Rows.Count != 0)
            _parameterGrid.ActiveRow = _parameterGrid.Selected.Rows[0];

         if (!_paramaterButton.IsDroppedDown) {
            SetButtonText();
            RaiseParameterSelected();
            return;
         }

         _paramaterButton.CloseUp();
      }

      public void ShowError(string message) {
         _errorProvider.SetError(this, message);
         _paramaterButton.BackColor = Color.Red;
      }

      public void ClearError() {
         _errorProvider.Clear();
         _paramaterButton.ResetBackColor();
      }

      public void SelectDefaultParameter() {
         try {
            _parameterGrid.Rows[0].Selected = true;
         }
         catch (Exception e) {
            throw;
         }
      }

      private void OnOpenedUp(object sender, CancelEventArgs e) {
         ClearError();
      }
   }
}
