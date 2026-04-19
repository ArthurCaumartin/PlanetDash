public class Health : Damagable
{
    public float _maxHealth = 100;
    public float _currentHealth = 100;

    public override void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
            Destroy(gameObject);
    }
}
