namespace XF.UI.Smart
{
   public class FakeObject
   {
      private string _name;
      private string _title;

      public FakeObject(string name, string title)
      {
         _name = name;
         _title = title;
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