using System;
using System.Reflection;

public static class UpgradeFamilyReader
{
    public static int GetFamily<T>(this T upgrade) where T : ScriptableUpgrade
    {
        Type t = upgrade.GetType();
        UpgradeFamilyAtribute atr = t.GetCustomAttribute<UpgradeFamilyAtribute>(false);
        if (atr == null)
        {
            return (int)UpgradeFamily.None;
        }
        return (int)atr.family;
    }
}