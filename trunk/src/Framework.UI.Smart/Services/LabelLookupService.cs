using System;
using System.Collections.Generic;

namespace XEVA.Framework.UI.Smart
{
   public class LabelLookupService : ILabelLookupService
   {
      private readonly ILabelStore _store;
      private IDictionary<string, string> _labels = new Dictionary<string, string>();

      public LabelLookupService(string location) : this(new XmlFileLabelStore())
      {
         _store.Location = location;
      }

      public LabelLookupService(ILabelStore store)
      {
         _store = store;
      }

      public bool LabelExists(string lookupKey)
      {
         return (_labels.ContainsKey(lookupKey));
      }

      public void RegisterLabel(string lookupKey, string label)
      {
         if (_labels.ContainsKey(lookupKey)) 
            _labels[lookupKey] = label;
         else
            _labels.Add(lookupKey, label);
      }

      public string GetLabel(string lookupKey)
      {
         if (_labels.ContainsKey(lookupKey)) return _labels[lookupKey];

         _labels[lookupKey] = lookupKey;
         
         return GetLabel(lookupKey);
      }

      public void SaveLabels()
      {
         _store.PersistLabels(_labels);
      }

      public void ReadLabels()
      {
         _labels = _store.ReadLabels();
      }
   }
}