using System;

namespace XF
{
   public class EventArgs<TArg> : EventArgs
   {
      private TArg _value;

      public EventArgs(TArg value)
      {
         _value = value;
      }

      public TArg Value
      {
         get { return _value; }
      }

   }
}