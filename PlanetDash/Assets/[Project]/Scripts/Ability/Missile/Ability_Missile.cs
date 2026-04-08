
using System;
using UnityEngine;

[Serializable]
public class Ability_Missile : AbilityDecorator
{
    private Missile _missilePrefab;
    public float damage;
    public int missileCount;

    public Ability_Missile(IAbility ability, Missile missilePrefab) : base(ability)
    {
        Debug.Log("Construct Ability Missile !");
        _missilePrefab = missilePrefab;
    }

    public void SetStat(float damage, int missileCount)
    {
        this.damage = damage;
        this.missileCount = missileCount;
    }

    public override void OnDashHit(Health healthHit, Health[] healthsHitArray)
    {
        base.OnDashHit(healthHit, healthsHitArray);

        for (int i = 0; i < missileCount; i++)
        {
            Missile m = GameObject.Instantiate(_missilePrefab,
                                            healthHit.transform.position + healthHit.transform.up,
                                            healthHit.transform.rotation
            );

            m.Init(healthsHitArray.GetRandome(), 50, 10, 150, 3);
        }
    }
}

