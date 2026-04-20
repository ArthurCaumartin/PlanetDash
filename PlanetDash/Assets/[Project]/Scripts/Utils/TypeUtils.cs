using System;
using System.Collections.Generic;
using UnityEngine;

public static class TypeUtils
{
    public static bool ContaineType<T>(this List<T> list, Type type)
    {
        foreach (var item in list)
        {
            if (item.GetType() == type)
                return true;
        }
        return false;
    }

    public static List<Type> GetInheranceCompo(this object obj, bool printCompo = false)
    {
        List<Type> composition = new List<Type>();

        Type current = obj.GetType();
        while (current != null)
        {
            composition.Add(current);
            current = current.BaseType;
        }
        composition.Reverse();

        if (printCompo)
        {
            foreach (var item in composition)
            {
                Debug.Log(item);
            }
        }

        return composition;
    }
}
