using MonoMod;

namespace FezGame.Tools
{
    // just for public read access
    [MonoModIgnore]
    public static class ThreadExecutionState
    {
        public static extern void TearDown();
    }
}
