using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace XF {
   public class ConfigFileHelper {
      private XElement _config;

      public ConfigFileHelper() {}

      public ConfigFileHelper(XElement config) {
         _config = config;
      }

      public void Initialize(string configFilePath) {
         _config = XElement.Load(configFilePath);
      }

      public void Initialize(XElement configFile) {
         _config = configFile;
      }

      public string ExtractOutput(XElement element) {
         return element.Value;
      }

      public List<TConfig> ExtractOutput<TConfig>(XElement configParent, string infoName)
         where TConfig : IConfigInfo {
         var result = new List<TConfig>();
         foreach (var element in configParent.Elements(XName.Get(infoName))) {
            var info = Activator.CreateInstance<TConfig>();
            info.Initialize(element);
            result.Add(info);
         }

         return result;
      }

      public List<TConfig> ExtractOutput<TConfig>(string infoName)
         where TConfig : IConfigInfo {
         var result = new List<TConfig>();
         foreach (var element in _config.Elements(XName.Get(infoName))) {
            var info = Activator.CreateInstance<TConfig>();
            info.Initialize(element);
            result.Add(info);
         }

         return result;
      }

   }
}