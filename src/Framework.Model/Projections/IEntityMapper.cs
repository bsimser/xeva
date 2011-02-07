using System.Collections.Generic;

namespace XF.Model
{
   public interface IEntityMapper : IHaveCriteriaMapper
   {
      int ParameterIdx { get; set; }
      int JoinRefIdx { get; set; }
      int EntityLevel { get; }
      void AddParameterPart(ProjectionPart parameterPart);
      void AddReferencePart(IReferencePart referencePart);
      List<IExpressionMapper> Citerion { get; }
      List<IOrderingMapper> Ordering { get; }
   }
}