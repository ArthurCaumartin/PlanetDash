using UnityEngine;

public class GameTimeControler : MonoBehaviour
{
    public static float Scale;
    [SerializeField] private float _gameFreezeOnMobHit = 0.2f;
    private float _gameFreezeTimer;

    private void Update()
    {
        if (_gameFreezeTimer > 0)
            Scale = 0;
        else
            Scale = 1;

        _gameFreezeTimer -= Time.unscaledDeltaTime;
        _gameFreezeTimer = Mathf.Clamp(_gameFreezeTimer, -0.01f, 1000);
    }


    private void OnMobHit(DamageInput damageInput)
    {
        _gameFreezeTimer += _gameFreezeOnMobHit;
    }

    private void OnDashStart(PathData[] path)
    {
        _gameFreezeTimer = 1000;
    }

    private void OnDashEnd()
    {
        _gameFreezeTimer = 0;
    }

    private void OnEnable()
    {
        GameplayEvent.OnMobHit.RemoveListener(OnMobHit);
        GameplayEvent.OnDashStart.RemoveListener(OnDashStart);
        GameplayEvent.OnDashEnd.RemoveListener(OnDashEnd);

        GameplayEvent.OnMobHit.AddListener(OnMobHit);
        GameplayEvent.OnDashStart.AddListener(OnDashStart);
        GameplayEvent.OnDashEnd.AddListener(OnDashEnd);
    }

    private void OnDisable()
    {
        GameplayEvent.OnMobHit.RemoveListener(OnMobHit);
        GameplayEvent.OnDashStart.RemoveListener(OnDashStart);
        GameplayEvent.OnDashEnd.RemoveListener(OnDashEnd);
    }
}