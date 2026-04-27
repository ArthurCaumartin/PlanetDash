public class MobHealth : Health
{
    private MobBehavior _mobBehavior;

    private void Awake()
    {
        _mobBehavior = GetComponent<MobBehavior>();
    }

    public override void TakeDamage(float amount)
    {
        AudioManager.Instance.Play(AudioManager.Instance.clipHitMarker);
        GameplayEvent.OnMobHit.Invoke(new DamageInput(_mobBehavior, amount));
        base.TakeDamage(amount);
    }
}