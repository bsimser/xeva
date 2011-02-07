using System.Collections.Generic;
using System.Text;

namespace XF.Model
{
   public interface IExpressionMapper : IHaveCriteriaMapper
   {
      string EntityName { get; set; }
      List<string> CriteriaList { get; }
      string ConjoinWith { get; set; }
   }
}