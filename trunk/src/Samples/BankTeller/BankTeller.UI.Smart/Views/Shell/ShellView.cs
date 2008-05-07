using System.Windows.Forms;
using BankTeller.UI.Smart.Presenters;

namespace BankTeller.UI.Smart.Views
{
   public partial class ShellView : UserControl, IShellView
   {
      private IShellCallbacks _callback;

      public ShellView()
      {
         InitializeComponent();
      }

      public void RegisterTool(string key, string label)
      {
         var toolItem = toolStrip1.Items.Add(label);
         toolItem.Click += (o, e) => _callback.ToolSelected(key);
      }

      public object UI
      {
         get { return this; }
      }

      public void Attach(IShellCallbacks callback)
      {
         _callback = callback;
      }
   }
}