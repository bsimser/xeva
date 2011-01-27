using System;
using System.Drawing;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace XF.Controls {
   public sealed class EditableGridHelper {
      public static void AddColumnButton(UltraGrid grid, int band, GridButton button) {
         var colName = button.ToString();
         var colWidth = button == GridButton.Edit ||
                        button == GridButton.Add
                           ? 34
                           : button == GridButton.Delete
                                ? 46
                                : button == GridButton.Override
                                     ? 60
                                     : 50;
            
         if (grid.DisplayLayout.Bands[band].Columns.IndexOf(colName) != -1) return;

         grid.DisplayLayout.Bands[band].Columns.Insert(grid.DisplayLayout.Bands[band].Columns.Count, colName);
         var gridColumn = grid.DisplayLayout.Bands[band].Columns[colName];
         gridColumn.ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
         gridColumn.CellAppearance.TextHAlign = HAlign.Center;
         gridColumn.CellAppearance.FontData.Name = "Tahoma";
         gridColumn.CellAppearance.FontData.SizeInPoints = 8f;
         gridColumn.CellActivation = Activation.NoEdit;
         gridColumn.CellClickAction = CellClickAction.CellSelect;
         gridColumn.Header.Caption = "";
         gridColumn.Header.Enabled = false;
         gridColumn.RowLayoutColumnInfo.PreferredCellSize = new Size(colWidth, 18);
         gridColumn.Style = ColumnStyle.Button;
      }

      public static void RemoveColumnButton(UltraGrid grid, int band, GridButton button) {
         var colName = button.ToString();
         var idx = grid.DisplayLayout.Bands[band].Columns.IndexOf(colName);
         if (idx != -1) grid.DisplayLayout.Bands[band].Columns.Remove(idx);
      }

      public static void SetButtonText(UltraGridRow row, GridButton button) {
         var colName = button.ToString();
         var label = button.ToString();
         if (row.Cells.IndexOf(colName) == -1) return;

         var buttonLabel = !string.IsNullOrEmpty(label) ? string.Format("[{0}]", label.ToLower()) : string.Empty;
         row.Cells[colName].Value = buttonLabel;
      }

      public static void CreateAddRow(UltraGrid grid) {
         grid.DisplayLayout.Bands[0].AddNew();
      }

      public static Point GetPositionForAddButton(UltraGrid grid) {
         var gridLoc = grid.Location;
         var colIdx = grid.DisplayLayout.Bands[0].Columns.Count - 1;
         var element = grid.DisplayLayout.Bands[0].Columns[colIdx].Header.GetUIElement();
         if(element != null) {
            var elementLoc = element.Rect.Location;
            var elementSize = element.Rect.Size;

            return new Point(elementLoc.X + elementSize.Width + gridLoc.X, elementLoc.Y + gridLoc.Y);
         }
         
         return new Point(gridLoc.X + grid.Size.Width, gridLoc.Y);
      }
   }

   public enum GridButton {
      Add, Edit, Delete, Remove, Override
   }

}