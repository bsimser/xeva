
namespace XEVA.Framework.UI.Smart
{
   public class FormBuilder : IFormBuilder
   {
      public void CreateLayout(string viewName, IPresenter presenter, string layoutType)
      {
         ControllerBuilder.Configure()
            .AddPresenter(viewName, presenter)
            .Layout.New(layoutType)
            .Command.Enable().IsDefault()
            .Run();
      }
   }
}
