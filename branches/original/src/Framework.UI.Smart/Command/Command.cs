using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace XEVA.Framework.UI.Smart
{
   public class Command : ICommand
   {
      private string _key;
      private CommandDelegate _callback = NoCallback;
      private bool _enabled = false;
      private bool _visible = false;
      private string _label;
      private ICommand _parent = null;
      private readonly Dictionary<string, ICommand> _children = new Dictionary<string, ICommand>();

      public virtual bool Enabled
      {
         get { return _enabled; }
         set
         {
            bool original = _enabled;

            _enabled = value;

            if (original != _enabled) OnEnabledChanged();
         }
      }

      public virtual string Key
      {
         get { return this._key; }
         set { this._key = value; }
      }

      public virtual string Label
      {
         get { return this._label; }
         set { this._label = value; }
      }

      public virtual bool Visible
      {
         get { return this._visible; }
         set
         {
            bool original = this._visible;

            this._visible = value;

            if (original != this._visible) this.OnVisibleChanged();
         }
      }

      public virtual void Execute()
      {
         if (!_enabled)
            return;

         CancelEventArgs args = new CancelEventArgs();
         OnExecuting(args);
         if (args.Cancel)
            return;

         _callback();

         OnExecuted();
      }

      public virtual CommandDelegate Callback
      {
         set { _callback = value ?? NoCallback; }
      }

      public virtual event CancelEventHandler Executing;

      public virtual event EventHandler Executed;

      public virtual event EventHandler EnabledChanged;

      public virtual event EventHandler VisibleChanged;

      protected virtual void OnExecuting(CancelEventArgs args)
      {
         if (Executing != null)
            Executing(this, args);
      }

      protected virtual void OnExecuted()
      {
         if (this.Executed != null)
            this.Executed(this, EventArgs.Empty);
      }

      protected virtual void OnEnabledChanged()
      {
         if (EnabledChanged != null)
            EnabledChanged(this, EventArgs.Empty);
      }

      protected virtual void OnVisibleChanged()
      {
         if (VisibleChanged != null)
            VisibleChanged(this, EventArgs.Empty);
      }

      private static void NoCallback() {}

      public void Show()
      {
         this.Visible = true;
      }

      public void Hide()
      {
         this.Visible = false;
      }

      public virtual IDictionary<string, ICommand> Children
      {
         get
         {
            return this._children;
         }
      }

      public ICommand Parent
      {
         get
         {
            return this._parent;
         }
         set
         {
            if (_parent != null) _parent.Children.Remove(this.Key);
            _parent = value;
            if (_parent.Children.ContainsKey(this.Key)) return;
            _parent.Children.Add(this.Key, this);
         }
      }
   }
}