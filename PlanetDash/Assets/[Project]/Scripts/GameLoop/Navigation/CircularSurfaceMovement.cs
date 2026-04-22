using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class CircularSurfaceMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1;
    [SerializeField] private float _jumpDuration = 0.5f;
    [SerializeField] private float _jumpHeight = 5;
    [SerializeField] private float _fallingSpeed = 5;
    [SerializeField] private float _gravityScale = 1;
    private float _altitude;
    private Task _dashSequenceTask;
    private Task _jumpTask;
    private float _currentFallingSpeed = 1;
    protected float currentAngleRadian;
    protected Vector2 velocity;
    protected PlanetSurface planetSurface;
    protected bool isGrounded = false;

    public Vector3 Velocity => velocity;
    public PlanetSurface CurrentSurface => planetSurface;


    protected virtual void Awake()
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
        ComputePositionAndRotation(currentAngleRadian);
    }


    protected void ComputeInitialeAngleAndAltitude()
    {
        _altitude = planetSurface.Radius + (Vector3.Distance(transform.position, planetSurface.transform.position) - planetSurface.Radius);
        Vector3 dirSurfaceToThis = (transform.position - planetSurface.transform.position).normalized;
        currentAngleRadian = -Vector3.SignedAngle(dirSurfaceToThis,
                                                   planetSurface.transform.right,
                                                   planetSurface.transform.forward) * Mathf.Deg2Rad;
    }

    protected void ComputePositionAndRotation(float radianAngle)
    {
        Vector3 newPosition = new Vector3(
            Mathf.Cos(radianAngle),
            Mathf.Sin(radianAngle),
            0
        );

        if (_altitude < planetSurface.Radius + 0.01)
        {
            isGrounded = true;
            _currentFallingSpeed = 1;
            if (_jumpTask == null)
                _altitude = planetSurface.Radius;
        }
        else
        {
            isGrounded = false;
            if (_jumpTask == null)
            {
                _currentFallingSpeed += Time.deltaTime * _fallingSpeed;
                _altitude -= Time.deltaTime * _gravityScale * _currentFallingSpeed;
            }
        }

        newPosition *= _altitude;
        newPosition += planetSurface.transform.position;
        transform.position = newPosition;

        if (velocity.x != 0)
        {
            Vector3 surfaceNormal = (transform.position - planetSurface.transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(Vector3.forward * velocity.x, surfaceNormal);
        }
    }

    protected void Jump()
    {
        if (!isGrounded || _jumpTask != null) return;
        _jumpTask = JumpAnimation(_jumpDuration, _jumpHeight);
    }

    public void SetSurface(PlanetSurface surface, float? angle = null, float? altitude = null)
    {
        planetSurface = surface;
        ComputeInitialeAngleAndAltitude();
        if (angle != null) currentAngleRadian = (float)angle;
        if (altitude != null) _altitude = (float)altitude;
    }

    public void MoveOnPath(PathData[] path, float duration, Action<float> toDoOnMove = null)
    {
        if (_dashSequenceTask != null) return;
        _dashSequenceTask = MoveOnPathAnimation(path, duration, toDoOnMove);
    }

    private async Task JumpAnimation(float duration, float height)
    {
        float startHeight = _altitude;
        float target = _altitude + height;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            _altitude = Mathf.Lerp(startHeight, target, Mathf.InverseLerp(0, duration, time));
            await Task.Yield();
        }
        _jumpTask = null;
    }

    private async Task MoveOnPathAnimation(PathData[] path, float duration, Action<float> toDoOnMove = null)
    {
        _jumpTask = null;

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