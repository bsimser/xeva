namespace XEVA.Framework.UI.Smart
{
   // A decorator for linking two commands
   public class LinkedCommand : Command
   {
      private readonly ICommand _targetCommand;
      private string _customLabel = null;
      public LinkedCommand(ICommand targetCommand)
      {
         this._targetCommand = targetCommand;
      }

      public override string Key
      {
         get
         {
            return _targetCommand.Key;
         }
         set
         {
            _targetCommand.Key = value;
         }
      }

      public override string Label
      {
         get
         {
            return _customLabel ?? _targetCommand.Label;
         }
         set
         {
            _customLabel = value;
         }
      }

      public override bool Visible
      {
         get
         {
            return this._targetCommand.Visible;
         }
         set
         {
            this._targetCommand.Visible = value;
         }
      }

      public override bool Enabled
      {
         get
         {
            return this._targetCommand.Enabled;
         }
         set
         {
            this._targetCommand.Enabled = value;
         }
      }

      public override void Execute()
      {
         _targetCommand.Execute();
      }
   }
}