using System;

namespace XF.Model
{
   public interface ITransaction : IDisposable
   {
      void Rollback();

      void Commit();
   }
}