using System;
using MonoMod;

namespace FezGame.Services
{
    public class GameStateManager
    {
        [MonoModReplaceString("FEZ", "FEZ_TAS")]
        [MonoModIgnore]
        public extern void SignInAndChooseStorage(Action onFinish);
    }
}
