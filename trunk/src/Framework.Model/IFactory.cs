namespace XF.Model
{
   public interface IFactory<TEntity> 
      where TEntity : IEntity
   {
      TEntity Generate(IBuilder entityBuilder);
   }
}