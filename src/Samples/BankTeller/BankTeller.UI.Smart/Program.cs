using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Castle.Windsor;
using XF;

namespace BankTeller.UI.Smart
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         
         Application.Run(new ShellContext());
      }


   }
}