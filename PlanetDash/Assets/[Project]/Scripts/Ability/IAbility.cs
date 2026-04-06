
public interface IAbility
{
    public void OnDashStart(PathData[] pathDatas);
    public void OnDashMove(float movementTime);
    public void OnDashHit(Health health);
    public void OnDashEnd();
}

