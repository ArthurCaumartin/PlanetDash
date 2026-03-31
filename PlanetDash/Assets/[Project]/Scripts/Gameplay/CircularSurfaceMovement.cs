using System.Collections;
using UnityEngine;

public abstract class CircularSurfaceMovement : MonoBehaviour
{
    [SerializeField] private float _movmentSpeed = 1;
    [SerializeField] private float _jumpDuration = 0.5f;
    [SerializeField] private float _gravityScale = 1;
    private float _currentAngle;
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
        _currentAngle -= (velocity.x * _movmentSpeed * Time.deltaTime) / planetSurface.Radius
                         * 180 / Mathf.PI; //* garentie le deplacement en m/s plutot qu'en deg/s
        ComputePosition();
    }

    private void ComputeInitialeAngle()
    {
        Vector3 dirSurfaceToThis = (transform.position - planetSurface.transform.position).normalized;
        _currentAngle = Vector3.SignedAngle(planetSurface.transform.right,
                                            dirSurfaceToThis,
                                            planetSurface.transform.forward);
    }

    private void ComputePosition()
    {
        Vector3 newPosition = new Vector3(
            Mathf.Cos(_currentAngle * Mathf.Deg2Rad),
            Mathf.Sin(_currentAngle * Mathf.Deg2Rad),
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

        Vector3 worldPos = planetSurface.transform.InverseTransformPoint(newPosition);
        transform.position = worldPos;
        transform.up = (transform.position - planetSurface.transform.position).normalized;
    }

    public void SetSurface(PlanetSurface surface)
    {
        planetSurface = surface;
        ComputeInitialeAngle();
    }

    protected void Jump()
    {
        if(!isGrounded) return;
        _altitude += 4;
    }
}
