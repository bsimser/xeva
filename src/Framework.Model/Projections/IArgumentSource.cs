using System.Collections.Generic;

namespace XF.Model {
   public interface IArgumentSource {
      IDictionary<string, ProjectionPart> NamedArguments { get; }
   }
}