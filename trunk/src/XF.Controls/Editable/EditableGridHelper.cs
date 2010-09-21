using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace XF.Controls {
   public class EditableGridHelper {
      public static void AddColumnButton(Infragistics.Win.UltraWinGrid.UltraGrid grid, int band, GridButton button) {
         var colName = button.ToString();
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
         gridColumn.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(46, 0);
         gridColumn.Style = ColumnStyle.Button;
      }

      public static void RemoveColumnButton(Infragistics.Win.UltraWinGrid.UltraGrid grid, int band, GridButton button) {
         var colName = button.ToString();
         var idx = grid.DisplayLayout.Bands[band].Columns.IndexOf(colName);
         if (idx != -1) grid.DisplayLayout.Bands[band].Columns.Remove(idx);
      }

      public static void SetButtonText(Infragistics.Win.UltraWinGrid.UltraGridRow row, GridButton button, string label) {
         var colName = button.ToString();
         if (row.Cells.IndexOf(colName) == -1 ) return;

         var buttonLabel = !string.IsNullOrEmpty(label) ? string.Format("[{0}]", label.ToLower()) : string.Empty;
         row.Cells[colName].Value = buttonLabel;
      }
   }

   public enum GridButton {
      Add, Edit, Delete
   }

}