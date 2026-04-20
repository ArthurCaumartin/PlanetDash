using System;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

public static class UpgradePriorityReader
{
    public static int GetPriority<T>(this T upgrade) where T : ScriptableUpgrade
    {
        Type t = upgrade.GetType();
        UpgradePriorityAttribute attribute =
        t.GetCustomAttribute<UpgradePriorityAttribute>(false);
        if (attribute == null)
        {
            Debug.LogError(upgrade.name + " no attribute found !");
            return (int)UpgradePriority.None;
        }

        return (int)attribute.Priority;
    }
}