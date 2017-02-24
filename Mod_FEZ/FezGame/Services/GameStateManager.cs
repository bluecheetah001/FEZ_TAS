using System;
using MonoMod;
using TasRules;

namespace FezGame.Services
{
    public class GameStateManager
    {
        [MonoModIgnore]
        [ReplaceString("FEZ", "FEZ_TAS")]
        public extern void SignInAndChooseStorage(Action onFinish);
    }
}
