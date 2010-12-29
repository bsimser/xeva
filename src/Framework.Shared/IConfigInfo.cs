using System.Xml.Linq;

namespace XF {
   public interface IConfigInfo {
      void Initialize(XElement element);
   }
}