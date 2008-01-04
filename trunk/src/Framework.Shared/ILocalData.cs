using System;

namespace XEVA.Framework
{
   public interface ILocalData
   {
      object this[object key] { get; set; }

      void Clear();
   }
}