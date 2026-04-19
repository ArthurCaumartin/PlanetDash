using System.Linq;
using UnityEngine;


public class Abiltiy_MissileJump : Ability_Missile
{
    [SerializeField] private float _detectionRadius = 20;
    [SerializeField] private LayerMask _detectionLayerMask;

    public Abiltiy_MissileJump(IAbility ability, Missile missilePrefab, float detectionRadius, LayerMask detectionLayerMask) : base(ability, missilePrefab)
    {
        _detectionRadius = detectionRadius;
        _detectionLayerMask = detectionLayerMask;
    }

    // public override void OnDashHit(Health healthHit, Health[] healthsHitArray)
    // {
    //     base.OnDashHit(healthHit, healthsHitArray);
    // }

    public override void OnJump()
    {
        base.OnJump();
        Damagable[] healths = PhysicsCastUtils2D.GetTypeInOverlapCircle<Damagable>(transform.position, _detectionRadius, _detectionLayerMask);
        SpawnMissile(transform.position, transform.rotation, healths.Length > 0 ? healths.GetRandom() : null);
    }
}