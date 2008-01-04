using System;
using System.Collections.Generic;
using XEVA.Framework.UI.Smart.ControllerMethodChain;

namespace XEVA.Framework.UI.Smart
{
   public class ControllerBuilder : IDynamicController,
                             IAfterConfigure,
                             IAddPresenter,
                             IPresenterOptions,
                             IAddCommand,
                             IWireUp,
                             IPresenterCommandOptions,
                             IAfterPresenterCommandOption,
                             IPresenterResolverOptions,
                             IDone
   {
      public static string DEFAULT_LAYOUT_IOC_KEY = "Layouts.Default";
      public static string DEFAULT_LAYOUT_KEY = "Default";
      private readonly ComponentKeyLookupService _componentKeyLookupService;
      private readonly IContext _context;
      private readonly IDictionary<string, ILayout> _layouts;
      private readonly Stack<ICommand> _commands;
      private string _defaultCommandKey = string.Empty;

      private ControllerBuilder()
      {
         this._context = new Context();
         this._componentKeyLookupService = new ComponentKeyLookupService();

         this._layouts = new Dictionary<string, ILayout>();
         this.AddSharedLayout(DEFAULT_LAYOUT_KEY, DEFAULT_LAYOUT_IOC_KEY);

         this._commands = new Stack<ICommand>();
      }

      public static IDynamicController Empty
      {
         get { return Configure().Done; }
      }

      #region IAfterConfigure Members

      public IDynamicController Done
      {
         get { return this; }
      }

      public IAfterConfigure AddSharedLayout(string layoutKey, ILayout layout)
      {
         string standardizedLayoutKey = layoutKey.ToLower();

         if (this._layouts.ContainsKey(standardizedLayoutKey)) return this;

         _layouts.Add(standardizedLayoutKey, layout);

         return this;
      }

      public IAfterConfigure AddSharedLayout(string layoutKey, string componentKey)
      {
         string standardizedLayoutKey = layoutKey.ToLower();

         if (this._layouts.ContainsKey(standardizedLayoutKey)) return this;

         string standardizedComponentKey =
            this._componentKeyLookupService.StandardizeComponentKey(ComponentKeyType.Layout, componentKey);

         ILayout layout = IoC.Resolve<ILayout>(standardizedComponentKey);

         _layouts.Add(standardizedLayoutKey, layout);

         return this;
      }

      public IPresenterOptions AddPresenter(string commandKey, string componentKey)
      {
         string standardizedComponentKey =
            this._componentKeyLookupService.StandardizeComponentKey(ComponentKeyType.Presenter, componentKey);

         IPresenter presenter = IoC.Resolve<IPresenter>(standardizedComponentKey);

         SetupPresenter(commandKey, presenter);

         return this;
      }

      public IPresenterOptions AddPresenter(string commandKey, IPresenter presenter)
      {
         SetupPresenter(commandKey, presenter);

         return this;
      }

      public IAddCommand AddCommand(string commandKey, string componentKey)
      {
         string standardizedComponentKey =
            this._componentKeyLookupService.StandardizeComponentKey(ComponentKeyType.Command, componentKey);

         ICommand command = IoC.Resolve<ICommand>(standardizedComponentKey);

         this.AddCommand(commandKey, command);

         return this;
      }

      public IAddCommand AddCommand(string commandKey, ICommand command)
      {
         command.Key = commandKey;

         _commands.Push(command);

         return this;
      }

      public IWireUp SetState(string key, object value)
      {
         this._context[key] = value;
         return this;
      }

      public IWireUp CreateLink(string sourceCommandKey, string targetCommandKey)
      {
         ICommand sourceCommand = FindCommand(sourceCommandKey);
         if (sourceCommand == null) throw new CommandNotFoundException();

         ICommand targetCommand = FindCommand(targetCommandKey);
         if (targetCommand == null) throw new CommandNotFoundException();

         ViewCommand sourceViewCommand = sourceCommand as ViewCommand;
         if (sourceViewCommand == null) throw new RequiresViewCommandException();

         sourceViewCommand.AddLink(new LinkedCommand(targetCommand));

         return this;
      }

      #endregion

      #region IController Members

      public ILayout FindLayout(string layoutKey)
      {
         string standardizedLayoutKey = layoutKey.ToLower();

         if (!_layouts.ContainsKey(standardizedLayoutKey)) return null;

         return _layouts[standardizedLayoutKey];
      }

      public void SaveLayout(string layoutKey, ILayout layout)
      {
         if (!_layouts.ContainsKey(layoutKey))
         {
            _layouts.Add(layoutKey, layout);
            return;
         }
         _layouts.Add(layoutKey, layout);
      }

      public void Run()
      {
         if (string.IsNullOrEmpty(_defaultCommandKey)) 
            throw new UnspecifiedDefaultCommand();

         Run(_defaultCommandKey);
      }

      public void Run(string commandKey)
      {
         ICommand command = FindCommand(commandKey);
         command.Execute();
      }

      #endregion

      #region IPresenterCommandOptions Members

      public IAfterPresenterCommandOption Label(string label)
      {
         this.LastCommand.Label = label;
         return this;
      }

      public IAfterPresenterCommandOption Enable()
      {
         this.LastCommand.Enabled = true;
         return this;
      }

      public IAfterPresenterCommandOption Disable()
      {
         this.LastCommand.Enabled = false;
         return this;
      }

      private ICommand LastCommand
      {
         get { return _commands.Peek(); }
      }

      public IAfterPresenterCommandOption IsDefault()
      {
         _defaultCommandKey = this.LastCommand.Key;
         return this;
      }

      #endregion

      #region IPresenterResolverOptions Members

      public IPresenterOptions Default()
      {
         ViewCommand command = this.LastCommand as ViewCommand;
         if (command == null) return this;

         command.LayoutResolver = new DefaultLayoutResolver(this);

         return this;
      }

      public IPresenterOptions Shared(string layoutKey)
      {
         ViewCommand command = this.LastCommand as ViewCommand;
         if (command == null) return this;
         command.LayoutResolver = new SharedLayoutResolver(this, layoutKey);
         return this;
      }

      public IPresenterOptions New(string layoutComponentKey)
      {
         string standardizedKey =
            _componentKeyLookupService.StandardizeComponentKey(ComponentKeyType.Layout, layoutComponentKey);

         ViewCommand command = this.LastCommand as ViewCommand;
         if (command == null) return this;
         command.LayoutResolver = new NewTemporaryLayoutResolver(standardizedKey);
         return this;
      }

      public IPresenterOptions Custom(ILayoutResolver resolver)
      {
         ViewCommand command = this.LastCommand as ViewCommand;
         if (command == null) return this;
         command.LayoutResolver = resolver;
         return this;
      }

      public IPresenterOptions Custom(ILayout layout)
      {
         ViewCommand command = this.LastCommand as ViewCommand;
         if (command == null) return this;
         command.LayoutResolver = new InternalResolver(layout);
         return this;
      }

      #endregion

      #region IPresenterOptions Members

      public IPresenterResolverOptions Layout
      {
         get { return this; }
      }

      public IPresenterCommandOptions Command
      {
         get { return this; }
      }

      #endregion

      public static IAfterConfigure Configure()
      {
         return new ControllerBuilder();
      }

      private ICommand FindCommand(string key)
      {
         List<ICommand> commandList = new List<ICommand>(_commands);

         Predicate<ICommand> finder =
            delegate(ICommand command)
            {
               return (command.Key.ToLower() == key.ToLower());
            };

         ICommand result = commandList.Find(finder);

         return result;
      }

      private void SetupPresenter(string commandKey, IPresenter presenter)
      {
         // TODO: If empty, get these from MD by type of presenter. If not found in MD create a default
         string presenterKey = presenter.Key;
         string presenterLabel = presenter.Label;

         ViewCommand command = new ViewCommand(presenter);

         command.LayoutResolver = new DefaultLayoutResolver(this);
         command.Context = this._context;
         command.Key = commandKey;
         command.Label = presenterLabel;
         command.Enabled = true;
         command.Visible = true;

         AddCommand(commandKey, command);
      }
   }
}