using System;
using XF.Model;

namespace XF.Model
{
   public class FakeEntity : Entity
   {
      private Guid _id;
      private int _version;

      public FakeEntity()
      {
      }
   }
}