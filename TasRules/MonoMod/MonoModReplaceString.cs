using System;

namespace MonoMod
{
    [MonoModCustomAttribute("ReplaceString")] // MonoMod.MonoModRules::ReplaceString
    public class MonoModReplaceString : Attribute
    {
        public extern MonoModReplaceString(string a, string b);
    }
}
