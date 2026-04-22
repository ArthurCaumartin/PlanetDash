using UnityEngine;

public class Health : Damagable
{
    [SerializeField] private HealthBar _healthBar;
    public float _maxHealth = 100;
    public float _currentHealth = 100;

    public override void TakeDamage(float amount)
    {
        if(_healthBar) _healthBar.UpdateFill(_currentHealth / _maxHealth);
        _currentHealth -= amount;
        if (_currentHealth <= 0)
            Destroy(gameObject);
    }
}
