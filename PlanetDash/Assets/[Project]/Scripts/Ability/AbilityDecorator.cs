using System;
using UnityEngine;

[Serializable]
public class AbilityDecorator : IAbility
{
    [SerializeField] protected IAbility _wrappedAbility;
    public AbilityDecorator(IAbility ability) { _wrappedAbility = ability; }

    protected void SetStat() { }

    public Transform Transform { get => transform; set => transform = value; }
    [SerializeField] protected Transform transform;
    public virtual void OnDashStart(PathData[] pathDatas) { _wrappedAbility.OnDashStart(pathDatas); }
    public virtual void OnDashMove(float movementTime) { _wrappedAbility.OnDashMove(movementTime); }
    public virtual void OnDashHit(Damagable damagableHit, Damagable[] damagableHitsArray)
    {
        _wrappedAbility.OnDashHit(damagableHit, damagableHitsArray);
    }
    public virtual void OnDashEnd() { _wrappedAbility.OnDashEnd(); }
    public virtual void OnJump() { }
}