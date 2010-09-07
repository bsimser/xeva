using System;
using System.Collections.Generic;
using System.Windows.Forms;
using XF.UI.Smart;

namespace XF.Controls 
{
   public partial class EditSaveCancel : UserControl 
   {
      private List<IEditable> _registeredControls = new List<IEditable>();

      private Dictionary<string,List<IEditable>> _groupedControls= new Dictionary<string, List<IEditable>>();

      private Dictionary<string, object> _group = new Dictionary<string, object>();

      private string _currentControlsGroup;

      public EditSaveCancel() 
      {
         InitializeComponent();
         CurrentMode = Mode.View;
      }

      public event EventHandler SaveClick;
      public event EventHandler CancelClick;
      public event EventHandler EditClick;

      public void RegisterFormFields<TMessage>(IFieldsRegistry<TMessage> registry)
      {
         RegisterGroupFormFields<TMessage>("Default",registry);
         CurrentControlsGroup = "Default";
      }

      public void RegisterGroupFormFields<TMessage>(string groupName, IFieldsRegistry<TMessage> registry)
      {
         if(!_group.ContainsKey(groupName))
            _group.Add(groupName,registry);         
      }

      public Mode CurrentMode { get; set; }

      public string CurrentControlsGroup
      {
         get
         {
            return _currentControlsGroup;
         }
         set
         {
            if (value == null) return;

            _currentControlsGroup = value;
            if (!_group.ContainsKey(value))
            {
               _registeredControls.Clear();
               return;
            }
            var controlsGroup = _group[value] as IFieldsRegistryList;
            _registeredControls = controlsGroup.RegisteredControls;
         }
      }

      public TMessage ReturnMessage<TMessage>(string groupName)
      {
         var result = Activator.CreateInstance<TMessage>();
         if (!_group.ContainsKey(groupName)) return result;

         var registry = _group[groupName] as IFieldsRegistry<TMessage>;
         return registry.GetHydratedMessage();
      }

      private void OnCancelClick(object sender, EventArgs e)
      {
         ClearValues();

         SetEditSaveCancelStates(true, false, false);
         CurrentMode = Mode.View;

         if (CancelClick!=null)  
            CancelClick(this, EventArgs.Empty);
      }

      public void ClearValues()
      {
         _registeredControls.ForEach(control =>
                                        {
                                           control.ResetValue();
                                           control.SetToEdit(false);
                                           control.ClearError();
                                        });
      }

      private void OnEditClick(object sender, EventArgs e) 
      {
         if (_registeredControls.Count == 0) return;

         SetToEdit();

         if (EditClick != null)
            EditClick(this, EventArgs.Empty);
      }

      private void OnSaveClick(object sender, EventArgs e) 
      {
         if (SaveClick == null) return;
         if (!_group.ContainsKey(CurrentControlsGroup)) return;

         var registry = _group[CurrentControlsGroup] as IFieldsRegistryList;

         if (!registry.Validate()) return;

         SetToView();

         if (SaveClick!=null)
            SaveClick(this, EventArgs.Empty);
      }

      public void SetToView()
      {
         _registeredControls.ForEach(control => control.SetToEdit(false));
         SetEditSaveCancelStates(true,false,false);
         CurrentMode = Mode.View;
      }

      public void SetToEdit()
      {
         _registeredControls.ForEach(control => control.SetToEdit(true));
         SetEditSaveCancelStates(false,true,true);
         CurrentMode = Mode.Edit;
      }

      private void SetEditSaveCancelStates(bool edit, bool save, bool cancel) {
         _btnEdit.Visible = edit;
         _btnSave.Visible = save;
         _btnCancel.Visible = cancel;
      }
   }
}
