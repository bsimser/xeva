namespace XF.Model
{
   public interface IProjector
   {
      int ProjectionIdx { get; set; }
      void AddParameterPart(ProjectionPart parameterPart);
      void AddReferencePart(IReferencePart referencePart);
   }
}