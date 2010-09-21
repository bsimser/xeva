using System;

namespace Model {
   public class XFCalculatorException : Exception {
      public XFCalculatorException(string message, Exception ex) : base(message, ex){
         
      }
   }
}