using MonoMod;
using TasRules;

namespace FezGame.Structure
{
    public class SaveManagementLevel
    {
        [ReplaceString("FEZ", "FEZ_TAS")]
        [MonoModIgnore]
        public extern void ChooseSaveSlot(SaveSlotInfo slot, SMOperation operation);

        [ReplaceString("FEZ", "FEZ_TAS")]
        [MonoModIgnore]
        public extern void ReloadSlots();

        [MonoModIgnore]
        public enum SMOperation { }
    }
}
