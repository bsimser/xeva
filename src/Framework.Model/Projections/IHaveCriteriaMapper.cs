using System.Collections.Generic;

namespace XF.Model {
   public interface IHaveCriteriaMapper {
      IDictionary<string, object> CriteriaParameters { get; set; }
   }
}