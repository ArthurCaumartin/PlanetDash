using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//TODO gravity and planet managment need to be more dynamique, more alive, need to see how to make that, very low priority
public static class PlanetUtils
{
    private static List<PlanetSurface> _surfaceList = new List<PlanetSurface>();
    public static void FetchAllSurfaceFromeScene()
    {
        // if (_surfaceList != null)
        //     return;
        _surfaceList = null;
        _surfaceList = GameObject.FindObjectsByType<PlanetSurface>(FindObjectsSortMode.None).ToList();
        // Debug.Log("Planet fetch count : " + _surfaceList.Count);
    }

    public static PlanetSurface GetNearest(Vector3 position)
    {
        if (_surfaceList == null || _surfaceList.Count == 0) return null;

        PlanetSurface toReturn = null;
        float minDist = Mathf.Infinity;
        for (int i = 0; i < _surfaceList.Count; i++)
        {
            float currentDistance = Vector3.Distance(position, _surfaceList[i].transform.position);
            if (currentDistance < minDist)
            {
                toReturn = _surfaceList[i];
                minDist = currentDistance;
            }
        }
        return toReturn;
    }
}