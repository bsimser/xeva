using System.Reflection;

namespace XEVA.Framework.UI.Smart
{
   public class PresenterState
   {
      private PropertyInfo _propertyInfo;
      private IPresenter _presenter;

      public PresenterState(PropertyInfo propertyInfo, IPresenter presenter)
      {
         this._propertyInfo = propertyInfo;
         this._presenter = presenter;
      }

      public PropertyInfo PropertyInfo
      {
         get { return _propertyInfo; }
      }

      public IPresenter Presenter
      {
         get { return _presenter; }
      }

      public object PropertyValue
      {
         get
         {
            return _propertyInfo.GetValue(_presenter, null);
         }
      }
   }
}