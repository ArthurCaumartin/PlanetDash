
using UnityEngine;

public class Ability_Missile : AbilityDecorator
{
    public Ability_Missile(IAbility ability) : base(ability) { }

    public override void OnDashHit(Health health)
    {
        base.OnDashHit(health);
        GameObject go = new GameObject("Missile");
        go.transform.position = health.transform.position;
    }
}

