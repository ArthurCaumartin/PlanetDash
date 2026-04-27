using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;


public class DashController : MonoBehaviour
{
    [SerializeField] private DebugCondition _debug;
    [SerializeField] private LayerMask _detectionLayer;
    [SerializeField] private float _coolDownDuration = 1f;
    [SerializeField] private float _dashDamage = 50f;
    [SerializeField] private float _dashRange = 1f;
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _detectionRadius = 3;
    private CircularSurfaceMovement _circularSurfaceMovement;
    private float _coolDownTimer = 0;
    private Task _dashSequenceTask = null;

    public float DetectionRadius => _detectionRadius;
    public bool IsDashing => _dashSequenceTask != null;


    private void Awake()
    {
        _circularSurfaceMovement = GetComponent<CircularSurfaceMovement>();
    }

    private void Update()
    {
        if (_dashSequenceTask == null)
            _coolDownTimer += Time.deltaTime;
    }

    private void OnDash(InputValue value)
    {
        if (!enabled) return;
        if (_dashSequenceTask != null) return;
        if (value.Get<float>() > .5f && _coolDownTimer > _coolDownDuration)
        {
            // print("DashInput");
            _coolDownTimer = 0;
            _dashSequenceTask = DashSequence();
        }
    }

    private async Task DashSequence()
    {
        //TODO split with small fct for bettre reading :)
        _circularSurfaceMovement.enabled = false;
        PathData[] path = _circularSurfaceMovement.GetPathOnVelocityDirection(_dashRange, GetDetectionResolution());
        List<Damagable> detectedDamagable = new List<Damagable>();

        GameplayEvent.OnDashStart.Invoke(path);

        for (int i = 1; i < path.Length; i++)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(path[i].position, _detectionRadius, _detectionLayer);
            // print(i + " : " + cols.Length);
            foreach (var item in cols)
            {
                Damagable dmgDetect = item.GetComponent<Damagable>();
                if (dmgDetect && !detectedDamagable.Contains(dmgDetect)) detectedDamagable.Add(dmgDetect);
            }
        }

        _circularSurfaceMovement.MoveOnPath(path, _dashDuration, (movementTime) => GameplayEvent.OnDashMove.Invoke(movementTime));

        await Task.Delay((int)(0.5 * 1000));

        foreach (var item in detectedDamagable)
        {
            item.TakeDamage(_dashDamage);
            GameplayEvent.OnDashHit.Invoke(item);
            await Task.Delay((int)(0.1 * 1000));
        }

        GameplayEvent.OnDashEnd.Invoke();
        _circularSurfaceMovement.enabled = true;
        _dashSequenceTask = null;
    }

    public int GetDetectionResolution()
    {
        return Mathf.Clamp((int)(_dashRange / 2), 2, 100);
    }

    private void OnDrawGizmos()
    {
        if(!_debug.enable) return;
        _circularSurfaceMovement = GetComponent<CircularSurfaceMovement>();
        if (!_circularSurfaceMovement) return;
        PathData[] path = _circularSurfaceMovement.GetPathOnVelocityDirection(_dashRange, GetDetectionResolution());
        if (path.Length == 0) return;
        // print("count : " + path.Length);
        // foreach (var item in path)
        //     print("path : " + item);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(path[0].position, 1);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(path[path.Length - 1].position, 1);
        for (int i = 1; i < path.Length; i++)
        {
            Gizmos.color = i % 2 == 0 ? Color.green : Color.yellow;
            Gizmos.DrawLine(path[i].position, path[i - 1].position);

            // Gizmos.color = i % 2 == 0 ? Color.green : Color.yellow;
            Gizmos.DrawWireSphere(path[i].position, _detectionRadius);
        }
    }
}
