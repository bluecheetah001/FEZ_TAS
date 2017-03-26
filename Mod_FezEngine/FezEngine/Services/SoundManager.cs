using MonoMod;

namespace FezEngine.Services
{
    class SoundManager
    {
        [MonoModReplaceString("FEZ", "FEZ_TAS")]
        [MonoModIgnore]
        public extern void InitializeLibrary();
    }
}
