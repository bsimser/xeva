namespace XF.UI.Smart
{
   public class FakeObject
   {
      private string _position;
      private string _name;
      private string _title;

      public FakeObject(string position, string name, string title)
      {
         _position = position;
         _name = name;
         _title = title;
      }

      public string Position
      {
         get { return _position; }
         set { _position = value; }
      }

      public string Name
      {
         get { return _name; }
         set { _name = value; }
      }

      public string Title
      {
         get { return _title; }
         set { _title = value; }
      }
   }
}