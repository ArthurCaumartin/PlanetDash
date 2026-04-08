
using System;

[Serializable]
public abstract class AbilityDecorator : IAbility
{
    protected IAbility _wrappedAbility;
    public AbilityDecorator(IAbility ability) { _wrappedAbility = ability; }

    protected void SetStat() { }

    public virtual void OnDashStart(PathData[] pathDatas) { _wrappedAbility.OnDashStart(pathDatas); }
    public virtual void OnDashMove(float movementTime) { _wrappedAbility.OnDashMove(movementTime); }
    public virtual void OnDashHit(Health healthHit, Health[] healthsHitArray) { _wrappedAbility.OnDashHit(healthHit, healthsHitArray); }
    public virtual void OnDashEnd() { _wrappedAbility.OnDashEnd(); }
}