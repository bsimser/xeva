using System.Collections.Generic;
using System.Text;

namespace XF.Model
{
   public interface IExpressionMapper
   {
      string EntityName { get; set; }
      IDictionary<string, object> CriteriaParameters { get; set; }
      List<string> CriteriaList { get; }
      string ConjoinWith { get; set; }
   }
}