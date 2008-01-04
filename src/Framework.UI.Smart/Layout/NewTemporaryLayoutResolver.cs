using System;
using System.Collections.Generic;

namespace XEVA.Framework.UI.Smart
{
   public class NewTemporaryLayoutResolver : ILayoutResolver
   {
      private readonly string _layoutComponentKey;
      private ILayout _layout;

      public NewTemporaryLayoutResolver(string layoutComponentKey)
      {
         if (string.IsNullOrEmpty(layoutComponentKey)) 
            throw new ArgumentException("layoutComponentKey");

         _layoutComponentKey = layoutComponentKey;
      }

      public ILayout GetLayout()
      {
         if (_layout == null) 
            _layout = IoC.Resolve<ILayout>(_layoutComponentKey);

         return _layout;
      }

   }
}