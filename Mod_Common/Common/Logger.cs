using MonoMod;
using TasRules;

namespace Common
{
    public class Logger
    {
        [ReplaceString("FEZ\\Debug Log.txt", "FEZ_TAS\\Debug Log.txt")]
        [MonoModIgnore]
        extern static Logger();
    }
}
