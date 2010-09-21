using System.Reflection;

namespace Model {
   public interface IToolKitAddin {
      MethodInfo GetMethodByName(string name);
   }
}