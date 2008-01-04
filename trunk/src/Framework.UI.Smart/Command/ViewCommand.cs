using System;
using System.Collections.Generic;
using System.Reflection;

namespace XEVA.Framework.UI.Smart
{
   public class ViewCommand : Command
   {
      private readonly IPresenter _presenter;
      private ILayoutResolver _layoutResolver;
      private readonly Dictionary<string, bool> _parameters = new Dictionary<string, bool>();

      private readonly Dictionary<string, PropertyInfo> _parameterPropertyList =
         new Dictionary<string, PropertyInfo>();

      private IContext _context;
      private readonly List<LinkedCommand> _links = new List<LinkedCommand>();

      public ViewCommand(IPresenter presenter)
      {
         _presenter = presenter;
         this.Callback = ShowPresenter;
         ScanPresenterForStartupParameters();
         ScanPresenterForCommands();
      }

      private void ScanPresenterForCommands()
      {
         ILabelLookupService lookup = IoC.Resolve<ILabelLookupService>();
         CommandScanner scanner = new CommandScanner(lookup);
         IList<ICommand> commands = scanner.ScanForCommands(_presenter);
         foreach (ICommand command in commands) command.Parent = this;
      }

      public ILayoutResolver LayoutResolver
      {
         set { this._layoutResolver = value; }
      }

      public IList<string> Parameters
      {
         get { return new List<string>(this._parameters.Keys); }
      }

      public IContext Context
      {
         set
         {
            if (this._context != null) this._context.StateChanged -= OnStateChanged;
            this._context = value;
            this._context.StateChanged += OnStateChanged;
            EvaluateEnabledState();
         }
      }

      public void AddLink(LinkedCommand link)
      {
         _links.Add(link);
      }

      private void OnStateChanged(object sender, EventArgs e)
      {
         EvaluateEnabledState();
      }

      private void EvaluateEnabledState()
      {
         foreach (KeyValuePair<string, bool> parameter in this._parameters)
         {
            string key = parameter.Key;
            bool required = parameter.Value;

            if ((!this._context.Contains(key)) && required)
            {
               base.Enabled = false;
               return;
            }
         }
         base.Enabled = true;
      }

      private void ScanPresenterForStartupParameters()
      {
         PropertyInfo[] properties = _presenter.GetType().GetProperties();

         foreach (PropertyInfo property in properties)
         {
            if (property.CanWrite)
            {
               object[] attributes = property.GetCustomAttributes(typeof (ParameterAttribute), true);
               foreach (object attribute in attributes)
               {
                  ParameterAttribute parameter = attribute as ParameterAttribute;
                  if (parameter != null && !this._parameters.ContainsKey(parameter.Key))
                  {
                     this._parameters.Add(parameter.Key, parameter.IsRequired);
                     this._parameterPropertyList.Add(parameter.Key, property);
                  }
               }
            }
         }
      }

      private void ShowPresenter()
      {
         if (this._layoutResolver == null) return;

         SetStartupParameters();

         _presenter.Start();

         _presenter.ParentForm = _layoutResolver.GetLayout();

         ILayout layout = (ILayout)_presenter.ParentForm;

         layout.ClearCommands();

         layout.DisplayPresenter(this._presenter);

         foreach(LinkedCommand linkedCommand in _links)
            layout.RegisterLinkCommand(linkedCommand);

         foreach (ICommand childCommand in this.Children.Values)
            layout.RegisterChildCommand(childCommand);

         layout.ShowLayout();
      }

      private void SetStartupParameters()
      {
         foreach (string key in this._parameters.Keys)
         {
            if (this._context.Contains(key))
            {
               object data = this._context[key];
               PropertyInfo property = this._parameterPropertyList[key];
               property.SetValue(_presenter, data, null);
            }
         }
      }
   }
}