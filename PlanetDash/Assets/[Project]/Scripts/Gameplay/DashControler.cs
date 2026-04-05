using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;


public class DashControler : MonoBehaviour
{
    [SerializeField] private float _coolDownDuration = 1f;
    [SerializeField] private float _dashRange = 1f;
    private CircularSurfaceMovement _circularSurfaceMovement;
    private float _coolDownTimer = 0;

    private void Awake()
    {
        _circularSurfaceMovement = GetComponent<CircularSurfaceMovement>();
    }

    private void Update()
    {
        _coolDownTimer += Time.deltaTime;
    }

    private void OnDash(InputValue value)
    {
        if (value.Get<float>() > .5f && _coolDownTimer > _coolDownDuration)
        {
            print("DashInput");
            _coolDownTimer = 0;
            _circularSurfaceMovement.Dash(_circularSurfaceMovement.Velocity.x, _dashRange, 0.2f, out PathData[] paths);
            
        }
    }

    private void OnDrawGizmos()
    {
        _circularSurfaceMovement = GetComponent<CircularSurfaceMovement>();
        if(!_circularSurfaceMovement) return;
        PathData[] path = _circularSurfaceMovement.GetPathOnDirection(_circularSurfaceMovement.Velocity.x, _dashRange, 10);
        if (path.Length == 0) return;
        // print("count : " + path.Length);
        // foreach (var item in path)
        //     print("path : " + item);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(path[0].position, 1);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(path[path.Length - 1].position, 1);
        for (int i = 0; i < path.Length - 1; i++)
        {
            Gizmos.color = i % 2 == 0 ? Color.green : Color.yellow;
            Gizmos.DrawLine(path[i].position, path[i + 1].position);
        }
    }
}