using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifier(Stat stat);
        IEnumerable<float> GetPercetegeModifier(Stat stat);
    }
}
