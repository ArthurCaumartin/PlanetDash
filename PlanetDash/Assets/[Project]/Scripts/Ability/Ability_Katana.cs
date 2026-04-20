using System;
using UnityEngine;

public class Ability_Katana : Ability
{
    [SerializeField] private float _damage = 10;
    [SerializeField] private float _attackRadius = 1;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private DebugCondition debug;
    [SerializeField] private Animator _animator;
    public float _attackPreSecond = 1;
    private float _attackTimer = 0;

    private void Start()
    {
        _animator.SetFloat("TriggerSpeed", _attackPreSecond);
    }

    protected override void Update()
    {
        if(dashController.IsDashing) return;
        base.Update();
        _attackTimer += Time.deltaTime;
        if (_attackTimer > 1 / _attackPreSecond)
        {
            Attack();
            _attackTimer = 0;
        }
    }

    private void Attack()
    {
        _animator.Play("AbilityTrigger");
        Damagable[] dmg = PhysicsCastUtils2D.GetTypeInOverlapCircle<Damagable>(transform.position, _attackRadius, _layerMask);
        for (int i = 0; i < dmg.Length; i++)
        {
            // print(name + " Hit : " + dmg[i].name);
            dmg[i].TakeDamage(_damage);
        }
    }

    private void OnDrawGizmos()
    {
        if (!debug.enable) return;

        Gizmos.color = debug.colorA;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}

