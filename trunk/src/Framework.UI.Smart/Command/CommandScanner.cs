using System;
using System.Collections.Generic;
using System.Reflection;

namespace XEVA.Framework.UI.Smart
{
   public class CommandScanner : ICommandScanner
   {
      private readonly ILabelLookupService _lookupService;

      public CommandScanner(ILabelLookupService lookupService)
      {
         this._lookupService = lookupService;
      }

      public IList<ICommand> ScanForCommands(object source)
      {
         List<ICommand> result = new List<ICommand>();
         Type type = source.GetType();

         MethodInfo[] methods = type.GetMethods();

         foreach (MethodInfo method in methods)
         {
            object[] attributes = method.GetCustomAttributes(typeof (CommandAttribute), true);
            foreach (object attribute in attributes)
            {
               CommandAttribute callbackAttribute = attribute as CommandAttribute;

               // TODO: Get the Key/Label service hooked in here too
               string commandKey = GetCommandKey(method);

               if ((method.ReturnType == typeof (void)) &&
                   (method.GetParameters().Length == 0))
               {
                  Command command = new Command();

                  command.Key = commandKey;

                  command.Label = GetCommandLabel(commandKey, method.Name);
                  command.Enabled = callbackAttribute.Enabled;
                  command.Visible = callbackAttribute.Visible;
                  command.Callback =
                        (CommandDelegate)Delegate.CreateDelegate(typeof (CommandDelegate), source, method.Name);
                  result.Add(command);
               }
            }
         }
         return result;
      }


      private string GetCommandKey(MethodInfo methodInfo)
      {
         return string.Format("{0}.{1}", methodInfo.DeclaringType.FullName, methodInfo.Name);
      }

      private string GetCommandLabel(string key, string defaultLabel)
      {
         if (_lookupService.LabelExists(key)) return _lookupService.GetLabel(key);
         _lookupService.RegisterLabel(key, defaultLabel);
         return _lookupService.GetLabel(key);
      }
   }
}