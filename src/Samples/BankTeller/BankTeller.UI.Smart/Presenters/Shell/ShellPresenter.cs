using System.Collections.Generic;
using BankTeller.UI.Smart.Services;
using XF.UI.Smart;

namespace BankTeller.UI.Smart.Presenters
{
   public class ShellPresenter : 
      Presenter<IShellView, IShellCallbacks>, 
      IShellCallbacks, IShellPresenter
   {
      private readonly IProfileService _profileService;

      public ShellPresenter(IProfileService profileService)
      {
         _profileService = profileService;
      }

      protected override void CustomStart()
      {
         var tools = _profileService.GetShellTools();

         if (tools == null) return;

         foreach(var tool in tools )
         {
            View.RegisterTool(tool.Key, tool.Value);
         }
      }

      public void ToolSelected(string key)
      {
         // Now... you can use this key to service locate (or use a local registry) a 
         // sub-triad. You can then lift off the IPresenter.UI and shove that in 
         // IShellView.DisplayTool(object UI)... This doesn't exist, but it's fairly
         // trivial to make happen.
      }
   }
}