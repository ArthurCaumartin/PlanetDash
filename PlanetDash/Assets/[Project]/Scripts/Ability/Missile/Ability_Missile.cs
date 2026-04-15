using System;
using UnityEngine;

[Serializable]
public class Ability_Missile : AbilityDecorator
{
    [SerializeField] protected Missile missilePrefab;
    [SerializeField] public float damage;
    [SerializeField] public int missileCount = 1;

    public Ability_Missile(IAbility ability, Missile missilePrefab) : base(ability)
    {
        // Debug.Log("Construct Ability Missile !");
        this.missilePrefab = missilePrefab;
    }

    public void SetStat(float damage, int missileCount)
    {
        this.damage = damage;
        this.missileCount = missileCount;
    }

    public override void OnDashHit(Health healthHit, Health[] healthsHitArray)
    {
        // Debug.Log("Missile : OnDash " + missileCount);
        base.OnDashHit(healthHit, healthsHitArray);

        for (int i = 0; i < missileCount; i++)
        {
            SpawnMissile(healthHit.transform, healthsHitArray.GetRandom());
        }
    }

    protected void SpawnMissile(Vector3 position, Quaternion rotation, Health target)
    {
        // Debug.Log("Spawn Missile : " + missilePrefab.name + " : target : " + target.name);
        Missile missile = GameObject.Instantiate(missilePrefab, position, rotation);
        missile.Init(target, 50, 10, 150, 3);
    }

    protected void SpawnMissile(Transform spawnTransform, Health target)
    {
        Missile missile = GameObject.Instantiate(missilePrefab, spawnTransform.position + spawnTransform.up, spawnTransform.rotation);
        missile.Init(target, 50, 10, 150, 3);
    }
}
