using System;

namespace XF {
   public class XFCalculatorException : Exception {
      public XFCalculatorException(string message, Exception ex) : base(message, ex){
         
      }
   }
}