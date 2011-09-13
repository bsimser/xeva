using System;
using XF.Model;

namespace XF.Model
{
   public class FakeEntity : Entity
   {
      [ModelCopy(Method = CopyMethod.Copy)]
      public virtual string Name { get; set; }
      [ModelCopy(Method = CopyMethod.Copy)]
      public virtual string Ttile { get; set; }
      [ModelCopy(Method = CopyMethod.None)]
      public virtual int Age { get; set; }
      [ModelCopy(Method = CopyMethod.None)]
      public virtual DateTime DOB { get; set; }

      public void SetDate(object obj) {
         Age = (int) obj;
      }
   }
}