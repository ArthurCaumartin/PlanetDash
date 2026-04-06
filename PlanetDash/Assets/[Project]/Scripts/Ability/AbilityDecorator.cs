
public abstract class AbilityDecorator : IAbility
{
    protected IAbility _wrappedAbility;
    public AbilityDecorator(IAbility ability) { _wrappedAbility = ability; }

    public virtual void OnDashStart(PathData[] pathDatas) { _wrappedAbility.OnDashStart(pathDatas); }
    public virtual void OnDashMove(float movementTime) { _wrappedAbility.OnDashMove(movementTime); }
    public virtual void OnDashHit(Health health) { _wrappedAbility.OnDashHit(health); }
    public virtual void OnDashEnd() { _wrappedAbility.OnDashEnd(); }
}

