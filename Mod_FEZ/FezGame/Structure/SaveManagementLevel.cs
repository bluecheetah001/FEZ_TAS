using MonoMod;

namespace FezGame.Structure
{
    public class SaveManagementLevel
    {
        [MonoModReplaceString("FEZ", "FEZ_TAS")]
        [MonoModIgnore]
        public extern void ChooseSaveSlot(SaveSlotInfo slot, SMOperation operation);

        [MonoModReplaceString("FEZ", "FEZ_TAS")]
        [MonoModIgnore]
        public extern void ReloadSlots();

        [MonoModIgnore]
        public enum SMOperation { }
    }
}
