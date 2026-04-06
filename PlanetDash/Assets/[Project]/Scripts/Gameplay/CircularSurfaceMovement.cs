using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class CircularSurfaceMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1;
    [SerializeField] private float _jumpDuration = 0.5f;
    [SerializeField] private float _gravityScale = 1;
    private float _altitude;
    private Task _dashSequenceTask;


    protected float currentAngleRadian;
    protected Vector2 velocity;
    protected PlanetSurface planetSurface;
    protected bool isGrounded = false;

    public Vector3 Velocity => velocity;


    protected virtual void Start()
    {
        planetSurface = PlanetUtils.GetNearest(transform.position);
        ComputeInitialeAngleAndAltitude();
    }

    private void OnEnable()
    {
        if (!planetSurface) return;
        ComputeInitialeAngleAndAltitude();
    }

    protected virtual void Update()
    {
        if (_dashSequenceTask != null) return;
        if (!planetSurface) return;

        // Negative sign reverses tangential direction
        // decrease angle = move right / increase angle = move left
        currentAngleRadian -= (velocity.x * _movementSpeed * Time.deltaTime) / planetSurface.Radius;
        ComputePosition(currentAngleRadian);
    }


    protected void ComputeInitialeAngleAndAltitude()
    {
        _altitude = planetSurface.Radius + (Vector3.Distance(transform.position, planetSurface.transform.position) - planetSurface.Radius);
        Vector3 dirSurfaceToThis = (transform.position - planetSurface.transform.position).normalized;
        currentAngleRadian = -Vector3.SignedAngle(dirSurfaceToThis,
                                                   planetSurface.transform.right,
                                                   planetSurface.transform.forward) * Mathf.Deg2Rad;
    }

    protected void ComputePosition(float radianAngle)
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

    protected void Jump()
    {
        if (!isGrounded) return;
        _altitude += 4;
    }

    public void SetSurface(PlanetSurface surface)
    {
        planetSurface = surface;
        ComputeInitialeAngleAndAltitude();
    }

    public void MoveOnPath(PathData[] path, float duration, Action<float> toDoOnMove = null)
    {
        if (_dashSequenceTask != null) return;
        _dashSequenceTask = MoveOnPathAnimation(path, duration, toDoOnMove);
    }

    private async Task MoveOnPathAnimation(PathData[] path, float duration, Action<float> toDoOnMove = null)
    {
        int targetIndex = 1;
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            float dashTime = i / duration;
            if (dashTime > path[targetIndex].time)
            {
                targetIndex++;
            }

            float travelTime = Mathf.InverseLerp(path[targetIndex - 1].time, path[targetIndex].time, dashTime);
            transform.position = Vector3.Lerp(path[targetIndex - 1].position, path[targetIndex].position, travelTime);
            transform.up = (transform.position - planetSurface.transform.position).normalized;

            toDoOnMove?.Invoke(dashTime);
            await Task.Yield();
        }
        ComputeInitialeAngleAndAltitude();
        _dashSequenceTask = null;
    }

    public PathData[] GetPathOnVelocityDirection(float distance, int resolution)
    {
        if (!planetSurface) return new PathData[] { };

        PathData[] path = new PathData[resolution];
        float startRad = currentAngleRadian;

        // Negative sign reverses tangential direction
        // decrease angle = move right / increase angle = move left
        float endRad = currentAngleRadian + ((distance / planetSurface.Radius) * -velocity.x);

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