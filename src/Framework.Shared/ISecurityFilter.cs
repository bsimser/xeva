using System;
using System.Collections.Generic;

namespace XF
{
   public interface ISecurityQueryService
   {
      System.Xml.Linq.XElement UserAccessibleInstances(string entityName, Guid userID);
   }
}