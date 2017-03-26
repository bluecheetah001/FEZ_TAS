using MonoMod;

namespace FezGame.Tools
{
    [MonoModIgnore]
    public static class ThreadExecutionState
    {
        public static extern void TearDown();
    }
}
