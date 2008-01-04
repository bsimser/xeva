using System;
using System.Collections.Generic;
using System.Xml;

namespace XEVA.Framework.UI.Smart
{
   public class XmlFileLabelStore : ILabelStore
   {
      private string _location;

      public string Location
      {
         set { _location = value; }
      }

      public void PersistLabels(IDictionary<string, string> labels)
      {
         XmlDocument document = new XmlDocument();
         document.LoadXml("<labels></labels>");

         foreach(KeyValuePair<string, string> item in labels)
         {
            XmlElement element = document.CreateElement(item.Key);
            element.InnerText = item.Value;
            document.DocumentElement.AppendChild(element);
         }

         document.Save(_location);
      }

      public IDictionary<string, string> ReadLabels()
      {
         Dictionary<string, string> result = new Dictionary<string, string>();

         XmlDocument document = new XmlDocument();
         document.Load(_location);

         XmlNodeList nodes = document.DocumentElement.ChildNodes;

         foreach (XmlNode node in nodes)
         {
            string key = node.Name;
            string label = node.InnerText;
            result.Add(key, label);
         }

         return result;
      }
   }
}