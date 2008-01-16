using System;
using System.Collections.Generic;
using System.Text;

namespace XF.Validation
{
   public interface IValidationAware
   {
      void ShowError(string messsage);

      void ClearError();
   }
}
