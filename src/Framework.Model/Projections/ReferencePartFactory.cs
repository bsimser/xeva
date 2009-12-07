
namespace XF.Model
{
   public static class ReferencePartFactory
   {
      public static IReferencePart GetReferencePart(ReferenceType referenceType)
      {
         if (referenceType == ReferenceType.CollectionPart)
            return new ReferenceCollectionPart();

         if (referenceType == ReferenceType.PropertyPart)
            return new ReferencePropertyPart();

         return new ReferenceTypePart();
      }
   }
}