using System;
using System.Collections.Generic;
using System.Text;

namespace XEVA.Framework.Validation
{
   public interface IValidationObject
   {
      void ShowError(string messsage);

      void ClearError();
   }
}
