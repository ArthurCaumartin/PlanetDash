using UnityEngine;

public static class ArrayUtils
{
    public static T GetRandom<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}
