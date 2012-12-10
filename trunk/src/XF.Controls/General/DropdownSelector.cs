using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using XF.UI.Smart;
using XF.Validation;
using System.Linq;

namespace XF.Controls {
   [ToolboxItem(true)]
   public partial class DropdownSelector : XFControlBase {
      private List<LookupMessage> _items;

      public DropdownSelector() {
         InitializeComponent();
         EditorControl = _internalButton;
         SetEditorControlPosition();
         DoubleBuffered = true;
      }

      public event EventHandler<EventArgs<LookupMessage>> ItemSelected;

      public List<LookupMessage> Items {
         set {
            _items = value;

            _items.Sort((a, b) => a.DisplayOrder.CompareTo(b.DisplayOrder));
            _itemsBS.SuspendBinding();
            _itemsBS.DataSource = new BindingAdapter<LookupMessage>(_items ?? new List<LookupMessage>());
            _itemsBS.ResumeBinding();
         }
      }

      public LookupMessage SelectedItem {
         get {
            return _itemsGrid.ActiveRow != null
                      ? _items.Find(item => item.Name.Equals(_itemsGrid.ActiveRow.Cells["Name"].Value))
                      : null;
         }
      }

      public void SetInitialSelection(LookupMessage message) {
         _itemsGrid.ActiveRow = _itemsGrid.Rows.Cast<UltraGridRow>().Where(r => (string)r.Cells["Name"].Value == message.Name).FirstOrDefault();
         Value = message.Name;
         RaiseItemSelected();
      }

      [Bindable(true)]
      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public override object Value {
         get { return _internalButton.Text; }
         set {
            if (value == null) return;

            var oldValue = _internalButton.Text;

            if (!string.IsNullOrEmpty(value.ToString()))
               _internalButton.Text = (value.ToString()).Replace("&", "&&");

            if (!_internalButton.Text.Equals(oldValue))
               base.OnValueChanged();
         }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public virtual Font ValueLabelFont {
         get { return _internalButton.Font; }
         set { _internalButton.Font = value; }
      }

      protected new void SetEditorControlPosition() {
         base.SetEditorControlPosition();
         _itemsGrid.Left = _internalButton.Left;
         _itemsGrid.Width = _internalButton.Width;
      }

      public void Clear() {
         _items = null;
         _itemsBS.SuspendBinding();
         _itemsBS.Clear();
         _internalButton.Text = string.Empty;
      }

      public override void ClearError() {
         _errorProvider.Clear();
      }

      private void OnControlLoad(object sender, System.EventArgs e) {
         _internalButton.DrawFilter = new DrawFilter();
      }

      private void OnClosedUp(object sender, EventArgs e) {
         SetButtonText();
         RaiseItemSelected();
      }

      private void OnOpeningUp(object sender, CancelEventArgs e) {
         ClearError();
      }

      private void RaiseItemSelected() {
         if (ItemSelected != null)
            ItemSelected(this, new EventArgs<LookupMessage>(SelectedItem));
      }

      private void SetButtonText() {
         if (_itemsGrid.ActiveRow != null)
            _internalButton.Text = SelectedItem.Name;
      }

      private class DrawFilter : IUIElementDrawFilter {
         public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams) {
            if (drawParams.Element is SplitButtonUIElement ||
               drawParams.Element is SplitButtonDropDownUIElement) {
               return DrawPhase.AfterDrawTheme;
            }

            return DrawPhase.None;
         }

         public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams) {
            // draw after the element theme as been drawn
            if (DrawPhase.AfterDrawTheme == drawPhase) {
               // get the button element
               var buttonElement = drawParams.Element as ButtonUIElementBase;

               if (buttonElement != null) {
                  // get the button control and current button state
                  var dropDown = drawParams.ControlElement.Control as UltraDropDownButton;
                  //var buttonState = buttonElement.ButtonState;
                  var buttonState = UIElementButtonState.MouseOver;

                  if (!dropDown.IsDroppedDown && !buttonElement.IsMouseOver) {

                     XPThemes.ToolBar.DrawToolbarButton(
                        false, drawParams.Element is SplitButtonDropDownUIElement,
                        buttonState, ref drawParams,
                        drawParams.Element.Rect, drawParams.InvalidRect);
                  }
               }
            }

            return false;
         }
      }

      private void OnLostFocus(object sender, EventArgs e) {
         _internalButton.CloseUp();
      }

      private void OnItemSelected(object sender, AfterSelectChangeEventArgs e) {
         if (_itemsGrid.Selected.Rows.Count != 0)
            _itemsGrid.ActiveRow = _itemsGrid.Selected.Rows[0];

         if (!_internalButton.IsDroppedDown) {
            SetButtonText();
            RaiseItemSelected();
            return;
         }

         if (_internalButton.IsDroppedDown) {
            _internalButton.CloseUp();
            return;
         }

      }
   }
}
