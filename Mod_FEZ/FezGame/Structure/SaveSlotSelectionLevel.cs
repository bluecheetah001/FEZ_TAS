using MonoMod;

namespace FezGame.Structure
{
    public class SaveSlotSelectionLevel
    {
        [MonoModReplaceString("FEZ", "FEZ_TAS")]
        [MonoModIgnore]
        public extern void Initialize();
    }
}
