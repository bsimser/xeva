using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using XF.UI.Smart;

namespace XF.Controls {
   [Designer(typeof(EditablePanelDesigner))]
   public partial class EditablePanel : UserControl, IHaveMessageToSave {

      private IFieldsRegistryList _registry;
      private bool _isInEdit = true;

      public EditablePanel() {
         InitializeComponent();
      }

      public event EventHandler EditClicked;
      public event EventHandler CancelClicked;
      public event EventHandler SaveClicked;

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      public GroupBox Panel1 { get { return _internalBox; } }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public string PanelText {
         get { return _internalBox.Text; }
         set { _internalBox.Text = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public bool Editable {
         get { return _isInEdit; }
         set {
            _isInEdit = value;
            _internalEdit.Visible = value;
         }
      }

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public object MessageToSave { get; set; }

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public bool PanelConfigured { get; set; }

      public void RegisterFields(IFieldsRegistryList registry) {
         _registry = registry;
         PanelConfigured = true;
      }

      public void Clear() {
         _registry.RegisteredControls.ForEach(cntrl => cntrl.Value = null);
      }

      public void BeginEdit() {
         ResetControlVisability(false);
         if (EditClicked != null)
            EditClicked(this, new EventArgs());
      }

      public void ShowCancel(bool showCancel) {
         _internalCancel.Visible = showCancel;
      }

      private void OnInternalEditClick(object sender, EventArgs e) {
         ResetControlVisability(false);
         if (EditClicked != null)
            EditClicked(this, new EventArgs());
      }

      private void OnCancelClick(object sender, EventArgs e) {
         CancelPanelEdit(true);
         if (CancelClicked != null)
            CancelClicked(this, new EventArgs());
      }

      private void OnSaveClick(object sender, EventArgs e) {
         if (_registry == null || !_registry.Validate()) return;

         SavePanelEdit(true);
         base.TabIndex = 0;
         if (SaveClicked != null)
            SaveClicked(this, new EventArgs());
      }

      private void OnLoad(object sender, EventArgs e) {
         _internalEdit.Location = new Point(_internalBox.Width - 30, -3);
         _internalCancel.Location = _internalEdit.Location;
         _internalSave.Location = new Point(_internalEdit.Location.X -30, -3);
         _toolTip.SetToolTip(_internalEdit, "Edit");
         if(!_isInEdit) _internalEdit.Visible = false;
         _toolTip.SetToolTip(_internalSave, "Save");
         _toolTip.SetToolTip(_internalCancel, "Cancel");
      }

      private void SavePanelEdit(bool isWaitingEdit) {
         if (_registry == null) return;

         _registry.RegisteredControls.ForEach(cnt => cnt.SaveValue());
         _registry.EditControls.ForEach(cnt => cnt.SaveValue());
         ResetControlVisability(isWaitingEdit);
      }

      private void CancelPanelEdit(bool isWaitingEdit) {
         if(_registry == null) return;

         _registry.RegisteredControls.ForEach(cnt => cnt.ResetValue());
         _registry.EditControls.ForEach(cnt => cnt.ResetValue());
         ResetControlVisability(isWaitingEdit);
      }

      private void ResetControlVisability(bool isWaitingEdit) {
         _internalBox.BackColor = isWaitingEdit ? Parent.BackColor : Color.LightYellow;
         _internalEdit.Visible = Editable ? isWaitingEdit : false;
         _internalSave.Visible = !isWaitingEdit;
         _internalCancel.Visible = !isWaitingEdit;
         if(_registry == null) return;
         _registry.RegisteredControls.ForEach(cnt => {
            cnt.SetToEdit(!isWaitingEdit);
            cnt.ControlBackcolor = !isWaitingEdit ? Color.LightYellow : Parent.BackColor;
         });
         _registry.EditControls.ForEach(cnt => {
            cnt.EnableEdit(!isWaitingEdit);
            cnt.ControlBackcolor = !isWaitingEdit ? Color.LightYellow : Parent.BackColor;
         });
      }
   }
}
