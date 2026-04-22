using UnityEngine;

public class MobMovement : CircularSurfaceMovement
{
    private int _direction;
    protected override void Awake()
    {
        base.Awake();
        _direction = Random.value > .5f ? 1 : -1;
        velocity.x = _direction;
    }
}
