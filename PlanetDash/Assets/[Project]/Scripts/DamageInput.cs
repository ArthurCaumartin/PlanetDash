public struct DamageInput
{
    public MobBehavior mobBehavior;
    public float amount;

    public DamageInput(MobBehavior mobBehavior, float amount)
    {
        this.mobBehavior = mobBehavior;
        this.amount = amount;
    }
}