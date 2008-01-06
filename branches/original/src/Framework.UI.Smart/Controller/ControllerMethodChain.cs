using System;

namespace XEVA.Framework.UI.Smart.ControllerMethodChain
{
   public interface IAfterConfigure : IAddLayout, IAddPresenter, IAddCommand, IWireUp, IDone
   {
      
   }

   public interface IAddLayout
   {
      IAfterConfigure AddSharedLayout(string layoutKey, ILayout layout);
      IAfterConfigure AddSharedLayout(string layoutKey, string componentKey);
   }

   public interface IDone : IController
   {
      IDynamicController Done { get; }
   }

   public interface IAddPresenter : IAddCommand, IWireUp, IDone
   {
      IPresenterOptions AddPresenter(string commandKey, string componentKey);
      IPresenterOptions AddPresenter(string commandKey, IPresenter presenter);  
   }

   public interface IAddCommand: IWireUp, IDone
   {
      IAddCommand AddCommand(string commandKey, string componentKey);
      IAddCommand AddCommand(string commandKey, ICommand command);
   }

   public interface IWireUp : IDone
   {
      IWireUp SetState(string key, object value);
      IWireUp CreateLink(string sourceCommandKey, string targetCommandKey);
   }


   public interface IPresenterOptions : IAddPresenter
   {
      IPresenterCommandOptions Command { get; }
      IPresenterResolverOptions Layout { get; }
   }

   public interface IPresenterCommandOptions
   {
      IAfterPresenterCommandOption Label(string label);
      IAfterPresenterCommandOption Enable();
      IAfterPresenterCommandOption Disable();
      IAfterPresenterCommandOption IsDefault();
   }

   public interface IPresenterResolverOptions 
   {
      IPresenterOptions Default();
      IPresenterOptions Shared(string layoutKey);
      IPresenterOptions New(string layoutComponentKey);
      IPresenterOptions Custom(ILayoutResolver resolver);
      IPresenterOptions Custom(ILayout layout);
   }


   public interface IAfterPresenterCommandOption : IAddCommand, IPresenterCommandOptions, IAddPresenter
   {
      
   }
   
}
