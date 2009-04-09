namespace XF.UI.Smart
{
   public interface IWindowOptions
   {
      bool Modal { get; set; }
      int Width { get; set; }
      int Height { get; set; }
      int Left { get; set; }
      int Top { get; set; }
      bool CloseConfirmation { get; set; }
      string ConfirmationMessage { get; set; }
      string ConfirmationCaption { get; set; }
      bool OpenInSecondMonitor { get; set; }
   }
}