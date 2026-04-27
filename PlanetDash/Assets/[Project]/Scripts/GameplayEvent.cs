using UnityEngine.Events;

public static class GameplayEvent
{
    public static UnityEvent<Damagable> OnDashHit = new UnityEvent<Damagable>();
    public static UnityEvent<PathData[]> OnDashStart = new UnityEvent<PathData[]>();
    public static UnityEvent<float> OnDashMove = new UnityEvent<float>();
    public static UnityEvent OnDashEnd = new UnityEvent();

    public static UnityEvent<DamageInput> OnMobHit = new UnityEvent<DamageInput>();
}
