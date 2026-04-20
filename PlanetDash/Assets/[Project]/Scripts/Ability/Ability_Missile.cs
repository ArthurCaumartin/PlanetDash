using UnityEngine;

public class Ability_Missile : Ability
{
    [SerializeField] protected Missile missilePrefab;
    [SerializeField] public float damage;


    protected override void OnDashHit(Damagable damagableHit)
    {
        base.OnDashHit(damagableHit);
        SpawnMissile(damagableHit.transform, damagableHit);
    }

    private void SpawnMissile(Transform spawnTransform, Damagable target)
    {
        Missile missile = Instantiate(missilePrefab, spawnTransform.position + spawnTransform.up, spawnTransform.rotation);
        missile.Init(target, 50, 10, 150, 3);
    }
}
