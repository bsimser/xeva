using System.Collections.Generic;

namespace XF.Model
{
   public interface IProjector
   {
      IDictionary<string, object> CriteriaParameters { get; set; }
      List<IExpressionMapper> Citerion { get; }
      ProjectionPart Parameters { get; }
      List<IReferencePart> References { get; }
      List<IOrderingMapper> Ordering { get; }
   }
}