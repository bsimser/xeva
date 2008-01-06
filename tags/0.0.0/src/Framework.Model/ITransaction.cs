using System;

namespace XEVA.Framework.Model
{
   public interface ITransaction : IDisposable
   {
      void Rollback();

      void Commit();
   }
}