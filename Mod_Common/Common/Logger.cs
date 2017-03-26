using MonoMod;

namespace Common
{
    public class Logger
    {
        [MonoModReplaceString("FEZ\\Debug Log.txt", "FEZ_TAS\\Debug Log.txt")]
        [MonoModIgnore]
        extern static Logger();
    }
}
