using UnityEngine;

public class DashVisual : MonoBehaviour
{
    [SerializeField] private DashController _dashControler;
    [SerializeField] private TrailRenderer _trailFxHitSequence;
    [SerializeField] private TrailRenderer _trailFxDashTravel;
    private PathData[] _currentDashPath;
    private TrailRenderer _trailHitSequenceInstance;
    private TrailRenderer _trailDashTravelInstance;

    private void Start()
    {
        _dashControler = GetComponent<DashController>();
        
        _dashControler.onDashStart.AddListener(OnDashStart);
        _dashControler.onDashHit.AddListener(OnHit);
        _dashControler.onDashEnd.AddListener(OnDashEnd);
    }

    public void OnDashStart(PathData[] path)
    {
        _currentDashPath = path;
        if (!_trailHitSequenceInstance)
            _trailHitSequenceInstance = Instantiate(_trailFxHitSequence, _currentDashPath[0].position, Quaternion.identity);

        if (!_trailDashTravelInstance)
            _trailDashTravelInstance = Instantiate(_trailFxDashTravel, transform);
        float radius = _dashControler.DetectionRadius / 2;
        _trailDashTravelInstance.widthCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, radius), new Keyframe(1, radius) });
    }

    public void OnHit(Health healthHit, Health[] healthsHitArray)
    {
        _trailHitSequenceInstance.transform.position = healthHit.transform.position;
    }

    public void OnDashEnd()
    {
        if (_trailHitSequenceInstance) Destroy(_trailHitSequenceInstance);
        if (_trailDashTravelInstance) Destroy(_trailDashTravelInstance);
    }

}