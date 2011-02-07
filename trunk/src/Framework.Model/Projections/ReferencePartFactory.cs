
namespace XF.Model
{
   public static class ReferencePartFactory
   {
      public static IReferencePart GetReferencePart(ReferenceType referenceType)
      {
         if (referenceType == ReferenceType.CollectionPart)
            return new ReferencePartCollection();

         if (referenceType == ReferenceType.PropertyPart)
            return new ReferencePartProperty();

         return new ReferencePartType();
      }
   }
}