using System;
using UnityEngine;

public static class PhysicsCastUtils2D
{
    public static T[] GetTypeInOverlapCircle<T>(Vector3 point, float raduis, LayerMask layerMask) where T : Component
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(point, raduis, layerMask);
        T[] foundArray = new T[cols.Length];
        for (int i = 0; i < cols.Length; i++)
        {
            T t = cols[i].GetComponent<T>();
            if (t) foundArray[i] = t;
        }
        foundArray = Array.FindAll(foundArray, x => x != null);
        return foundArray;
    }
}