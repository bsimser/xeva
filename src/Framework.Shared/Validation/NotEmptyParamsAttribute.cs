using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using XF.Validation;

namespace XF.Validation
{
   public class NotEmptyParamsAttribute : ValidationAttribute
   {
      public override string OptionalMessage { get; set; }

      protected override void Validate(object target, object rawValue, ValidationResult validationResult)
      {
         var parameters = XDocument.Parse(rawValue.ToString());

         var allElement = (from all in parameters.Elements("Parameters").Elements("All-But")
                           select all).First();

         var selectedElement = new List<XElement>(from selected in parameters.Elements("Parameters").Elements("Selected").Elements("Params")
                                                   select selected);

         if (allElement.Value == false.ToString() &&
             selectedElement.Count == 0)
            AddMessage(validationResult, "No parameters have been selected.");

      }

   }
}