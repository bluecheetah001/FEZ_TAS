using System;
using MonoMod;
using TasRules;

namespace FezGame.Services
{
    public class GameStateManager
    {
        [ReplaceString("FEZ", "FEZ_TAS")]
        [MonoModIgnore]
        public extern void SignInAndChooseStorage(Action onFinish);
    }
}
