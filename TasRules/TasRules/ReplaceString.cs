using System;
using MonoMod;

namespace TasRules
{
    [MonoModCustomAttribute("ReplaceString")] // MonoMod.MonoModRules::ReplaceString
    public class ReplaceString : Attribute
    {
        public extern ReplaceString(string a, string b);
    }
}
