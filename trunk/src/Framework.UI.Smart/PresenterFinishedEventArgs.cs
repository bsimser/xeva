using System;

namespace XF.UI.Smart
{
   public class PresenterFinishedEventArgs : EventArgs
   {
      public readonly string Key;

      public PresenterFinishedEventArgs(string key)
      {
         Key = key;
      }
   }
}