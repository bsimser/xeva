using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace XF.UI.Smart
{
   public abstract class Presenter<TView, TCallbacks> : IRefreshes, IRefreshable
      where TCallbacks : class, IViewCallbacks
      where TView : class, IView<TCallbacks>
   {
      private string _key;
      private string _label;
      private bool _activated;
      private bool _finished;
      private IPresenterValidator _presenterValidator;
      private IRequest _request;
      private TView _view;
      private IWindowAdapter _windowAdapter;
      private readonly Dictionary<string, IControl> _controls = new Dictionary<string, IControl>();
      private readonly List<IRefreshable> _refreshables = new List<IRefreshable>();

      public TView View
      {
         get { return _view; }
         set { _view = value; }
      }

      public string Key
      {
         get { return _key; }
         set { _key = value; }
      }

      public string Label
      {
         get { return _label; }
         set { _label = value; }
      }

      public WorkItemBuilder Queue
      {
         get
         {
            return new WorkItemBuilder(Locator.Resolve<IWorkItemDispatcher>());
         }
      }

      #region Presenter Lifecycle

      public void Activate()
      {
         Activate(_request ?? new NullRequest());
      }

      public void Activate(IRequest request)
      {
         _request = request;

         if (View == null) throw new ViewNotAvailableException();
         
         if (!_activated)
         {
            var callbacks = this as TCallbacks;
            if (callbacks == null) throw new NoCallbacksImplementationException();
            View.Attach(callbacks);
         }

         OnHandleRequest(request);
         if (!_activated) OnFirstActivation();
         OnEveryActivation();

         _activated = true;
 
         Window.Show();
      }

      public void Finish()
      {
         Finish(false);
      }

      private void Finish(bool windowInitiated)
      {
         if (!_activated) return;
         if (_finished) return;
         
         if (!windowInitiated && (_windowAdapter != null))
         {
            _windowAdapter.Closed -= OnWindowClosed;
            _windowAdapter.Close();
         }
         _windowAdapter = null;
         _finished = true;
         
         OnFinish();

         if (Finished != null)
            Finished(this, new PresenterFinishedEventArgs(Key));
      }

      public event EventHandler<PresenterFinishedEventArgs> Finished;

      #endregion

      #region Optional Lifecycle Hook Methods

      protected virtual void OnHandleRequest(IRequest request) { }

      protected virtual void OnFirstActivation() { }

      protected virtual void OnEveryActivation() { }

      protected virtual void OnFinish() { }

      #endregion

      public IWindowController Window
      {
         get { return (IWindowController) _windowAdapter ?? new NoWindowControls(); }
      }

      public virtual object UI
      {
         get
         {
            var result = (_view == null) ? null : _view.UI;
            return result;
         }
      }

      public void DisplayIn(IWindowManager manager, IWindowOptions options)
      {
         if (UI == null) throw new NoUserInterfaceObjectException();

         _windowAdapter = manager.CreateWindowFor(this);

         if (options != null) _windowAdapter.ApplyOptions(options);
         _windowAdapter.InitializeUI(UI);
         _windowAdapter.Closed += OnWindowClosed;

         if (_activated) _windowAdapter.Show();
      }

      public void DisplayIn(IWindowManager manager)
      {
         this.DisplayIn(manager, manager.CreateDefaultWindowOptionsFor(this));
      }

      private void OnWindowClosed(object sender, EventArgs e)
      {
         Finish(true);
      }


      #region Validation Helpers

      public Dictionary<string, IControl> Controls
      {
         get { return _controls; }
      }

      public void RegisterControl(string property, IControl control)
      {
         _controls.Add(property, control);
      }

      public void RegisterControl<TMessage>(Expression<Func<TMessage, Object>> expression, IControl control)
      {
         var mem = expression.Body as MemberExpression;
         var property = mem.Member.Name;
         _controls.Add(property, control);
      }

      public bool Validate(object target)
      {
         return Validate(new object[1] {target});
      }

      public bool Validate(object[] targets)
      {
         InitializeValidator(new PresenterValidator());
         return _presenterValidator.Validate(targets, _controls);
      }

      public virtual void InitializeValidator(IPresenterValidator presenterValidator)
      {
         if (_presenterValidator == null)
            _presenterValidator = presenterValidator;
      }

      #endregion

      #region Refresh/Refreshable Support

      public void Register(IRefreshable refreshable)
      {
         if (_refreshables.Contains(refreshable)) return;
         _refreshables.Add(refreshable);
      }

      public void RefreshAll()
      {
         _refreshables.ForEach(r => r.Refresh());
      }

      public virtual void Refresh() { }

      #endregion
   }
}