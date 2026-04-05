using UnityEngine;

public struct PathData
{
    public Vector3 position;
    public float time;
    public PathData(Vector3 position, float time)
    {
        this.position = position;
        this.time = time;
    }
}
