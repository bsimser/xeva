using System.Reflection;

namespace XF {
   public interface IToolKitAddin {
      MethodInfo GetMethodByName(string name);
   }
}