using XF.Model;

namespace XF.Store
{
   public class FakeNamedQuery : NamedQuery
   {
      public FakeNamedQuery()
      {
         Name = "FakeNamedQuery";
      }
   }
}