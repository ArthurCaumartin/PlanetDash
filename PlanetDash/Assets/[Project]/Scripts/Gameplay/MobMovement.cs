public class MobMovement : CircularSurfaceMovement
{
    protected override void Start()
    {
        base.Start();
        ComputePosition(currentAngleRadian);
    }

    protected override void Update()
    {
        
    }
}