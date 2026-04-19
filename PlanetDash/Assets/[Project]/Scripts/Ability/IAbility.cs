using System;
using UnityEngine;

public interface IAbility
{
    public Transform Transform { get; set; }
    public void OnDashStart(PathData[] pathDatas);
    public void OnDashMove(float movementTime);
    public void OnDashHit(Damagable damagableHit, Damagable[] damagableHitsArray);
    public void OnDashEnd();
    public void OnJump();
}

