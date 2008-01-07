using System;
using System.Collections.Generic;
using System.Text;

namespace XEVA.Framework.Validation
{
   public interface IValidationAware
   {
      void ShowError(string messsage);

      void ClearError();
   }
}
