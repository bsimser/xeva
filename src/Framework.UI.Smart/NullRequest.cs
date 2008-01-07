namespace XEVA.Framework.UI.Smart
{
   public class NullRequest : Request
   {
      public override bool IsNull
      {
         get { return true; }
      }
   }
}