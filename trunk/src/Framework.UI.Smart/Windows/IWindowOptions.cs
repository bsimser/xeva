namespace XF.UI.Smart
{
   public interface IWindowOptions
   {
      bool Modal { get; set; }
      int Width { get; set; }
      int Height { get; set; }
      int Left { get; set; }
      int Top { get; set; }
   }
}