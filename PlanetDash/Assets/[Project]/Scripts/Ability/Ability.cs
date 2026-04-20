using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected DashController dashController;

    public virtual void Init(DashController dashController)
    {
        this.dashController = dashController;
    }

    protected virtual void Update()
    {

    }

    protected virtual void OnDash(PathData[] path)
    {

    }

    protected virtual void OnDashHit(Damagable damagableHit)
    {

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
        GameplayEvent.OnDashStart.AddListener(OnDash);
        GameplayEvent.OnDashHit.AddListener(OnDashHit);
    }

    private void UnsubToGameplayEvent()
    {
        GameplayEvent.OnDashStart.RemoveListener(OnDash);
        GameplayEvent.OnDashHit.RemoveListener(OnDashHit);
    }
}
