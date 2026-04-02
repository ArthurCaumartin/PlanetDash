using System;
using System.Collections;
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

    protected virtual void Start()
    {
        planetSurface = PlanetUtils.GetNearest(transform.position);
        ComputeInitialeAngle();
    }

    protected virtual void Update()
    {
        if (!planetSurface) return;
        _currentAngleRadian -= (velocity.x * _movementSpeed * Time.deltaTime) / planetSurface.Radius;
        //* garentie le deplacement en m/s plutot qu'en deg/s
        ComputePosition();
    }

    private void ComputeInitialeAngle()
    {
        Vector3 dirSurfaceToThis = (transform.position - planetSurface.transform.position).normalized;
        _currentAngleRadian = -Vector3.SignedAngle(dirSurfaceToThis,
                                                   planetSurface.transform.right,
                                                   planetSurface.transform.forward) * Mathf.Deg2Rad;
    }

    private void ComputePosition()
    {
        Vector3 newPosition = new Vector3(
            Mathf.Cos(_currentAngleRadian),
            Mathf.Sin(_currentAngleRadian),
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

    public Vector3[] GetPathOnDirection(float direction, float distance, int resolution)
    {
        // return new Vector3[] { };
        if (!planetSurface) return new Vector3[] { };

        Vector3[] path = new Vector3[resolution];
        float startRad = _currentAngleRadian;
        float endRad = _currentAngleRadian + ((distance / planetSurface.Radius) * -direction);
        for (int i = 0; i < resolution; i++)
        {
            float radTime = Mathf.Lerp(startRad, endRad, Mathf.InverseLerp(0, resolution, i));
            Vector3 point = new Vector3(
                Mathf.Cos(radTime),
                Mathf.Sin(radTime),
                0
            );
            path[i] = (point * (_altitude + 2)) + planetSurface.transform.position;
        }
        return path;
    }
}
