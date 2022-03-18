using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModify
    {
        IEnumerable<float> GetModifier(Stat stat);
        IEnumerable<float> GetPercentModifier(Stat stat);
    }
}