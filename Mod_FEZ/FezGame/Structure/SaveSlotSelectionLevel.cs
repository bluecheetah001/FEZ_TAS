using MonoMod;
using TasRules;

namespace FezGame.Structure
{
    public class SaveSlotSelectionLevel
    {
        [ReplaceString("FEZ", "FEZ_TAS")]
        [MonoModIgnore]
        public extern void Initialize();
    }
}
