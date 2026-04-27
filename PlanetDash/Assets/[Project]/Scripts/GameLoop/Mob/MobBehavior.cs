using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;

public class MobBehavior : MonoBehaviour
{
    [SerializeField] private float _attackDelayMin;
    [SerializeField] private float _attackDelayMax;
    private float _currentAttackDelay;
    private Animator _animator;
    private MobMovement _mobMovement;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentAttackDelay = Random.Range(_attackDelayMin, _attackDelayMax);
        StartCoroutine(AttackCooldown(_currentAttackDelay));
    }

    private void Update()
    {
        // faire la boucle d'attaque / animation pour les movbs
    }

    private IEnumerator AttackCooldown(float duration)
    {
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        SetAttack();
    }

    private void SetAttack()
    {
        _animator.Play("Perp Attack");
    }
}
