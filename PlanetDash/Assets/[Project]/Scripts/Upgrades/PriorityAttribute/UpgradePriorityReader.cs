using System;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

public static class UpgradePriorityReader
{
    public static int GetPriority(Object upgrade)
    {
        Type t = upgrade.GetType();
        UpgradePriorityAttribute attribute =
        t.GetCustomAttribute<UpgradePriorityAttribute>(inherit: false);
        if (attribute == null)
        {
            Debug.LogError(upgrade.name + " no attribute found !");
            return -1;
        }

        return (int)attribute.Priority;
    }
}