using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using XF.UI.Smart;

namespace XF.Controls {
   public partial class ParameterListControl : UserControl, IControl {
      private const string ALL_PARAM = "(Select All)";
      private const string ALLBUT_PARAM = "(Select All But)";
      private const string NAME_PARAM = "Name";
      private const string ID_PARAM = "Id";
      private const string ALLBUT = "All-But";
      private const string PARAMETERS = "Parameters";
      private const string PARAMS = "Params";
      private const string SELECTED = "Selected";
      private const string UNIVERSAL = "Universal";
      private List<ParameterNameID> _parameters;
      private string _parameter = NAME_PARAM;

      public ParameterListControl() {
         InitializeComponent();
         InitializeParameterList();
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

      public List<ParameterNameID> Parameters {
         set {
            InitializeParameterList();
            _parameters.AddRange(value);

            _parameterBindingSource.SuspendBinding();
            _parameterBindingSource.DataSource = new BindingAdapter<ParameterNameID>(_parameters);
            _parameterBindingSource.ResumeBinding();

            ButtonText = string.Empty;
         }
      }

      public void ClearParameters() {
         _parameters.Clear();
         _parameters.Add(new ParameterNameID { Name = ALL_PARAM, ID = "all" });
      }

      public XElement IDParameterList {
         get { return BuildXElementParameterList(ID_PARAM); }
      }

      public XElement NameParameterList {
         get { return BuildXElementParameterList(NAME_PARAM); }
      }

      public List<string> SelectedIDLIST {
         get {
            var result = new List<string>();
            foreach (var row in _parameterGrid.Rows) {
               if (row.Cells[NAME_PARAM].Value.ToString() != ALL_PARAM &&
                   (bool)row.Cells[SELECTED].Value)
                  result.Add(row.Cells[ID_PARAM].Value.ToString());
            }
            return result;
         }
      }

      public List<string> SelectedNameLIST {
         get {
            var result = new List<string>();
            foreach (var row in _parameterGrid.Rows) {
               if (row.Cells[NAME_PARAM].Value.ToString() != ALL_PARAM &&
                   (bool)row.Cells[SELECTED].Value)
                  result.Add(row.Cells[NAME_PARAM].Value.ToString());
            }
            return result;
         }
      }

      public string ButtonText {
         set { _paramaterButton.Text = value; }
      }

      private XElement BuildXElementParameterList(string parameterName) {
         var result = new XElement(PARAMETERS);

         if (IsEmptyParameterList() || UnrestrictedSelection()) return DefaultParameterList();

         // Code was deprecated on 3/12/09 by RB
         //if (_parameters.Count > 0 &&
         //    string.IsNullOrEmpty(_parameters[0].ID)) return DefaultParameterList();
         //if (_parameters.Count == 1) return DefaultParameterList();

         result.Add(GetXElementForAllBut());
         result.Add(GetXElementForSelectedList(parameterName));
         result.Add(GetXElementForOriginalList(parameterName));
         return result;
      }

      private bool IsEmptyParameterList() {
         return _parameters.Count == 1;
      }

      private bool UnrestrictedSelection() {
         var allBut = false;
         foreach (var row in _parameterGrid.Rows) {
            if (row.Cells[NAME_PARAM].Value.ToString() == ALL_PARAM &&
                (bool)row.Cells[SELECTED].Value) {
               allBut = true;
               break;
            }
         }
         return allBut;
      }

      private XElement DefaultParameterList() {
         var result = new XElement(PARAMETERS);
         result.Add(new XElement(ALLBUT, "unrestricted"));
         result.Add(new XElement(SELECTED));
         result.Add(new XElement(UNIVERSAL));
         return result;
      }

      private XElement GetXElementForAllBut() {
         var allBut = false;
         foreach (var row in _parameterGrid.Rows) {
            if (row.Cells[NAME_PARAM].Value.ToString() == ALL_PARAM &&
                (bool)row.Cells[SELECTED].Value) {
               allBut = true;
               break;
            }
         }

         var result = new XElement(ALLBUT, allBut);

         return result;
      }

      private XElement GetXElementForSelectedList(string parameterName) {
         var selectedSet = new XElement(SELECTED);
         foreach (var row in _parameterGrid.Rows) {
            if (row.Cells[NAME_PARAM].Value.ToString() != ALL_PARAM &&
                (bool)row.Cells[SELECTED].Value)
               selectedSet.Add(new XElement(PARAMS, row.Cells[parameterName].Value.ToString()));
         }
         return selectedSet;
      }

      private XElement GetXElementForOriginalList(string parameterName) {
         var result = new XElement(UNIVERSAL);
         foreach (var row in _parameterGrid.Rows) {
            if (row.Cells[NAME_PARAM].Value.ToString() != ALL_PARAM)
               result.Add(new XElement(PARAMS, row.Cells[parameterName].Value.ToString()));
         }
         return result;
      }

      private void InitializeParameterList() {
         _parameters = new List<ParameterNameID> { new ParameterNameID { Name = ALL_PARAM, ID = "all" } };
      }

      private void OnClosedUp(object sender, EventArgs e) {
         SetButtonText();
         RaiseParameterSelected();
      }

      private void RaiseParameterSelected() {
         if (ParameterSelected != null)
            ParameterSelected(this, new EventArgs());
      }

      private void SetButtonText() {
         var text = new StringBuilder();
         foreach (var row in _parameterGrid.Rows) {
            if ((bool)row.Cells[SELECTED].Value) {
               if (text.Length > 0) text.Append(", ");
               text.Append(row.Cells[NAME_PARAM].Value.ToString());
            }
         }

         _paramaterButton.Text = text.ToString();
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

      private void SetSelectedStatusRemainingRows(string selectedParam) {
         foreach (UltraGridRow row in _parameterGrid.Rows) {
            if (selectedParam != ALL_PARAM &&
               row.Cells[NAME_PARAM].Value.ToString() == ALL_PARAM) {
               row.Cells[SELECTED].Value = false;
               return;
            }
            if (row.Cells[NAME_PARAM].Value.ToString() != ALL_PARAM &&
                row.Cells[NAME_PARAM].Value.ToString() != selectedParam)
               row.Cells[SELECTED].Value = false;
         }
      }

      public void ShowError(string message) {
         _errorProvider.SetError(this, message);
         _paramaterButton.BackColor = Color.Red;
      }

      public void ClearError() {
         _errorProvider.Clear();
         _paramaterButton.ResetBackColor();
      }

      private void OnMouseDown(object sender, MouseEventArgs e) {
         Point gridPoint = new Point(e.X, e.Y);
         UIElement uiElement = _parameterGrid.DisplayLayout.UIElement.ElementFromPoint(gridPoint);
         _parameterGrid.ActiveRow = uiElement.GetContext(typeof(UltraGridRow), true) as UltraGridRow;

         if (_parameterGrid.ActiveRow == null) return;

         _parameterGrid.ActiveRow.Cells[SELECTED].Value = !(bool)_parameterGrid.ActiveRow.Cells[SELECTED].Value;
         SetSelectedStatusRemainingRows(_parameterGrid.ActiveRow.Cells[NAME_PARAM].Value.ToString());
      }

      public void SelectDefaultParameter() {
         if (_parameterGrid.Rows == null) return;

         foreach (var parameterRow in _parameterGrid.Rows) {
            if (parameterRow.Cells[NAME_PARAM].Value.ToString() != ALL_PARAM) {
               parameterRow.Cells[SELECTED].Value = true;
               ClearError();
               SetButtonText();
               RaiseParameterSelected();
               return;
            }
         }
      }

      private void OnOpenedUp(object sender, CancelEventArgs e) {
         ClearError();
      }
   }
}
