namespace XF.Model
{
   public interface IProjector
   {
      int ParameterIdx { get; set; }
      int JoinRefIdx { get; set; }
      int EntityLevel { get; }
      void AddParameterPart(ProjectionPart parameterPart);
      void AddReferencePart(IReferencePart referencePart);
   }
}