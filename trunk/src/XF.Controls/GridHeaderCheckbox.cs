using System;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace XF.Controls {
   public class GridHeaderCheckbox : IUIElementCreationFilter {
      private static Infragistics.Win.UltraWinGrid.ColumnHeader _columnHeader;
      private static CheckState _checkState;
      private static RowsCollection _rowsCollection;

      public delegate void HeaderCheckBoxClickedHandler(object sender, HeaderCheckBoxEventArgs e);
      public event HeaderCheckBoxClickedHandler _CLICKED;

      public GridHeaderCheckbox() {
         _CLICKED += HeaderCheckBoxClicked;
      }

      public void UncheckHeader(UltraGrid grid) {
         if (_columnHeader == null) return;

         _columnHeader.Tag = CheckState.Unchecked;
         _checkState = CheckState.Unchecked;
      }

      private void HeaderCheckBoxClicked(object sender, HeaderCheckBoxEventArgs e) {
         if (e.Header.Column.DataType != typeof(bool)) return;

         foreach (UltraGridRow row in e.Rows) {
            if (row.ChildBands != null) {
               if (row.ChildBands.Count != 0) {
                  ToggleChildBandRows(row, e.CurrentCheckState);
               }
            }
            row.Cells[e.Header.Column.Index].Value = (e.CurrentCheckState == CheckState.Checked);
         }
      }

      private void ToggleChildBandRows(UltraGridRow parentRow, CheckState checkState) {
         foreach (UltraGridRow row in parentRow.ChildBands[0].Rows) {
            if (row.Cells.Exists("Selected"))
               row.Cells["Selected"].Value = (checkState == CheckState.Checked);
         }
      }

      public class HeaderCheckBoxEventArgs : EventArgs {
         public HeaderCheckBoxEventArgs(Infragistics.Win.UltraWinGrid.ColumnHeader columnHeader, CheckState checkState, RowsCollection rowsCollection) {
            _columnHeader = columnHeader;
            _checkState = checkState;
            _rowsCollection = rowsCollection;
         }

         public RowsCollection Rows {
            get { return _rowsCollection; }
         }

         public Infragistics.Win.UltraWinGrid.ColumnHeader Header {
            get { return _columnHeader; }
         }

         public CheckState CurrentCheckState {
            get { return _checkState; }
            set { _checkState = value; }
         }
      }

      public bool BeforeCreateChildElements(UIElement parent) {
         return false;
      }

      public void AfterCreateChildElements(UIElement parent) {
         if (!(parent is HeaderUIElement)) return;

         var header = ((HeaderUIElement)parent).Header;
         if (header.Column.Band.Index != 0) return;

         if (header.Column.DataType == typeof(bool)) {
            var checkBoxUIElement = (CheckBoxUIElement)parent.GetDescendant(typeof(CheckBoxUIElement)) ??
                                    new CheckBoxUIElement(parent);

            var textUIElment = (TextUIElement)parent.GetDescendant(typeof(TextUIElement));

            if (textUIElment == null) return;

            var columnHeader =
               (Infragistics.Win.UltraWinGrid.ColumnHeader)checkBoxUIElement.GetAncestor(typeof(HeaderUIElement))
                                                              .GetContext(typeof(Infragistics.Win.UltraWinGrid.ColumnHeader));

            if (columnHeader.Tag == null)
               columnHeader.Tag = CheckState.Unchecked;
            else
               checkBoxUIElement.CheckState = (CheckState)columnHeader.Tag;

            checkBoxUIElement.ElementClick += CheckBoxElementClick;

            parent.ChildElements.Add(checkBoxUIElement);

            var x = ((parent.Rect.Width - checkBoxUIElement.CheckSize.Width) / 2) + parent.Rect.X;
            var y = (parent.Rect.Height - checkBoxUIElement.CheckSize.Height) / 2;
            checkBoxUIElement.Rect = new Rectangle(x, y, checkBoxUIElement.CheckSize.Width, checkBoxUIElement.CheckSize.Height);

            textUIElment.Rect = new Rectangle(checkBoxUIElement.Rect.Right + 3, textUIElment.Rect.Y,
                                              parent.Rect.Width - (checkBoxUIElement.Rect.Right - parent.Rect.X), textUIElment.Rect.Height);
         }
         else {
            var checkBoxUIElement = (CheckBoxUIElement)parent.GetDescendant(typeof(CheckBoxUIElement));

            if (checkBoxUIElement != null) {
               parent.ChildElements.Remove(checkBoxUIElement);
               checkBoxUIElement.Dispose();
            }
         }
      }

      private void CheckBoxElementClick(Object sender, UIElementEventArgs e) {
         var checkBoxUIElement = (CheckBoxUIElement)e.Element;

         var columnHeader = (Infragistics.Win.UltraWinGrid.ColumnHeader)checkBoxUIElement.GetAncestor(typeof(HeaderUIElement)).GetContext(typeof(Infragistics.Win.UltraWinGrid.ColumnHeader));

         columnHeader.Tag = checkBoxUIElement.CheckState;

         var headerUIElement = checkBoxUIElement.GetAncestor(typeof(HeaderUIElement)) as HeaderUIElement;
         var rowsCollection = headerUIElement.GetContext(typeof(RowsCollection)) as RowsCollection;

         if (_CLICKED != null)
            _CLICKED(this, new HeaderCheckBoxEventArgs(columnHeader, checkBoxUIElement.CheckState, rowsCollection));
      }
   }
}