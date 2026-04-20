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

    public void OnHit(Damagable damagableHit)
    {
        _trailHitSequenceInstance.transform.position = damagableHit.transform.position;
    }

    public void OnDashEnd()
    {
        if (_trailHitSequenceInstance) Destroy(_trailHitSequenceInstance);
        if (_trailDashTravelInstance) Destroy(_trailDashTravelInstance);
    }


    protected virtual void OnEnable()
    {
        UnsubToGameplayEvent();
        SubToGameplayEvent();
    }

    protected virtual void OnDisable()
    {
        UnsubToGameplayEvent();
    }

    private void SubToGameplayEvent()
    {
        GameplayEvent.OnDashStart.AddListener(OnDashStart);
        GameplayEvent.OnDashEnd.AddListener(OnDashEnd);
        GameplayEvent.OnDashHit.AddListener(OnHit);
    }

    private void UnsubToGameplayEvent()
    {
        GameplayEvent.OnDashStart.RemoveListener(OnDashStart);
        GameplayEvent.OnDashEnd.RemoveListener(OnDashEnd);
        GameplayEvent.OnDashHit.RemoveListener(OnHit);
    }
}