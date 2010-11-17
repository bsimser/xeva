namespace XF {
   public sealed class MaskFactory {
      public static IMaskedType GetMaskImpl(MaskedType type) {
         switch (type)
         {
            case MaskedType.ControlNum:
               return new EINMaskType();
            case MaskedType.EIN:
               return new EINMaskType();
            case MaskedType.SSN:
               return new SSNMaskType();
            case MaskedType.Phone:
               return new PhoneMaskType();
            default:
               return new BlankMaskType();
         }
      }
   }
}