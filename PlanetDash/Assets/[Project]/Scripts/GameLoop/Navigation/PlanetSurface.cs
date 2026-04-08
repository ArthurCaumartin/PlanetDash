using System.Collections;
using UnityEngine;

public class PlanetSurface : MonoBehaviour
{
    [SerializeField] private Transform _visualPivot;
    [SerializeField] private float _planetRadius = 1;

    public float Radius => _planetRadius;

    private void Awake()
    {
        PlanetUtils.FetchAllSurfaceFromeScene();
        OnValidate();
    }

    private void OnValidate()
    {
        if (!_visualPivot) return;
        _visualPivot.localScale = Vector3.one * _planetRadius * 2;
    }

    public float GetSurfaceDistance(Vector3 position)
    {
        float distance = Vector3.Distance(position, transform.position) - Radius;
        return distance;
    }
}
