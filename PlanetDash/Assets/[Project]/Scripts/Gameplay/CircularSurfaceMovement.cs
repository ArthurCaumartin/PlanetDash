using System.Threading.Tasks;
using UnityEngine;

public abstract class CircularSurfaceMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1;
    [SerializeField] private float _jumpDuration = 0.5f;
    [SerializeField] private float _gravityScale = 1;
    private float _currentAngleRadian;
    private float _jumpDistance;
    private float _altitude;
    private float _dynamiqueGravityScale;

    protected Vector2 velocity;
    protected PlanetSurface planetSurface;
    protected bool isGrounded = false;

    public Vector3 Velocity => velocity;


    protected virtual void Start()
    {
        planetSurface = PlanetUtils.GetNearest(transform.position);
        ComputeInitialeAngle();
    }

    private void OnEnable()
    {
        if (!planetSurface) return;
        ComputeInitialeAngle();
    }

    protected virtual void Update()
    {
        if (_dashSequenceTask != null) return;
        if (!planetSurface) return;
        _currentAngleRadian -= (velocity.x * _movementSpeed * Time.deltaTime) / planetSurface.Radius;
        ComputePosition(_currentAngleRadian);
    }

    private void ComputeInitialeAngle()
    {
        Vector3 dirSurfaceToThis = (transform.position - planetSurface.transform.position).normalized;
        _currentAngleRadian = -Vector3.SignedAngle(dirSurfaceToThis,
                                                   planetSurface.transform.right,
                                                   planetSurface.transform.forward) * Mathf.Deg2Rad;
    }

    private void ComputePosition(float radianAngle)
    {
        Vector3 newPosition = new Vector3(
            Mathf.Cos(radianAngle),
            Mathf.Sin(radianAngle),
            0
        );

        if (_altitude < planetSurface.Radius + 0.01)
        {
            _altitude = planetSurface.Radius;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            _altitude -= Time.deltaTime * _gravityScale;
        }

        newPosition *= _altitude;
        newPosition += planetSurface.transform.position;
        transform.position = newPosition;
        transform.up = (transform.position - planetSurface.transform.position).normalized;
    }

    public void SetSurface(PlanetSurface surface)
    {
        planetSurface = surface;
        ComputeInitialeAngle();
    }

    protected void Jump()
    {
        if (!isGrounded) return;
        _altitude += 4;
    }

    private Task _dashSequenceTask;

    public void MoveOnPath(PathData[] path, float duration)
    {
        if (_dashSequenceTask != null) return;
        _dashSequenceTask = MoveOnPathAnimation(path, duration);
    }

    private async Task MoveOnPathAnimation(PathData[] path, float duration)
    {
        // foreach (var item in path)
            // print("PathData : " + item.position + " // " + item.time);

        int targetIndex = 1;
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            // print("Dash Time : " + i + " /// " + targetIndex);
            float dashTime = i / duration;
            if (dashTime > path[targetIndex].time)
                targetIndex++;

            float travelTime = Mathf.InverseLerp(path[targetIndex - 1].time, path[targetIndex].time, dashTime);
            transform.position = Vector3.Lerp(path[targetIndex - 1].position, path[targetIndex].position, travelTime);
            transform.up = (transform.position - planetSurface.transform.position).normalized;
            await Task.Yield();
        }
        ComputeInitialeAngle();
        _dashSequenceTask = null;
    }

    public PathData[] GetPathOnVelocityDirection(float distance, int resolution)
    {
        // return new Vector3[] { };
        if (!planetSurface) return new PathData[] { };

        PathData[] path = new PathData[resolution];
        float startRad = _currentAngleRadian;
        float endRad = _currentAngleRadian + ((distance / planetSurface.Radius) * -velocity.x);
        for (int i = 0; i < resolution; i++)
        {
            float pathTime = resolution > 1 ? i / (float)(resolution - 1) : 0f;
            float radTime = Mathf.Lerp(startRad, endRad, pathTime);
            Vector3 point = new Vector3(
                Mathf.Cos(radTime),
                Mathf.Sin(radTime),
                0
            );
            path[i] = new PathData((point * _altitude) + planetSurface.transform.position, pathTime);
        }
        return path;
    }

}



