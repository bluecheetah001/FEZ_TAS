using MonoMod;
using TasRules;

namespace FezEngine.Services
{
    class SoundManager
    {
        [ReplaceString("FEZ", "FEZ_TAS")]
        [MonoModIgnore]
        public extern void InitializeLibrary();
    }
}
