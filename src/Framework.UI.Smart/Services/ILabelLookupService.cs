namespace XEVA.Framework.UI.Smart
{
   public interface ILabelLookupService
   {
      bool LabelExists(string lookupKey);

      void RegisterLabel(string lookupKey, string label);

      string GetLabel(string lookupKey);

      void SaveLabels();

      void ReadLabels();
   }
}