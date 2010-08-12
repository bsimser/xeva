using System.Collections.Generic;

namespace XF.Model
{
   public interface IEntityMapper
   {
      int ParameterIdx { get; set; }
      int JoinRefIdx { get; set; }
      int EntityLevel { get; }
      void AddParameterPart(ProjectionPart parameterPart);
      void AddReferencePart(IReferencePart referencePart);
      IDictionary<string, object> CriteriaParameters { get; set; }
      List<IExpressionMapper> Citerion { get; }
      List<IOrderingMapper> Ordering { get; }
   }
}