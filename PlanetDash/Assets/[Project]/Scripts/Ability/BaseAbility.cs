using UnityEngine;

public class BaseAbility : IAbility
{
    public void OnDashStart(PathData[] pathDatas) { Debug.Log("Ability / Dash Start"); }
    public void OnDashMove(float movementTime) { Debug.Log("Ability / Dash Move : " + movementTime); }
    public void OnDashHit(Health health) { Debug.Log("Ability / Dash hit : " + health.name); }
    public void OnDashEnd() { Debug.Log("Ability / Dash End"); }
}

