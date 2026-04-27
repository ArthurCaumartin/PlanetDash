using UnityEngine;

public class Missile : Projectile
{
    [SerializeField] protected float speed = 10;
    [SerializeField] protected float trakingSpeed = 3;
    [SerializeField] protected float explosionRange = 3;
    private float _canDealDamage = 0.5f;
    protected Damagable target;


    public virtual Projectile Init(Damagable target,
                                   float damage,
                                   float speed,
                                   float trakingSpeed,
                                   float explosionRange)
    {
        base.Init(damage);
        this.target = target;
        this.damage = damage;
        this.speed = speed;
        this.trakingSpeed = trakingSpeed;
        this.explosionRange = explosionRange;

        Damagable randomTarget = TryDetectNewTarget();
        this.target = randomTarget ? randomTarget : target;

        // transform.Rotate(Vector3.forward, Random.value > .5f ? 50 : -50);

        return this;
    }
    float angle;
    protected override void Update()
    {
        base.Update();

        if (!target)
            target = TryDetectNewTarget();
        if (!target)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dirToTarget = (target.transform.position + target.transform.up) - transform.position;
        dirToTarget = dirToTarget.normalized;

        angle = Vector3.SignedAngle(transform.up, dirToTarget, Vector3.forward);
        transform.Rotate(Vector3.forward, Time.deltaTime * trakingSpeed * Mathf.Sign(angle));
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        _canDealDamage -= Time.deltaTime * GameTimeControler.Scale;
        if (_canDealDamage < 0)
            TryDetectDamagable();
    }

    private Damagable TryDetectNewTarget()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 100);
        if (cols.Length == 0) return null;
        for (int i = 0; i < cols.Length; i++)
        {
            Damagable h = cols[i].GetComponent<Damagable>();
            if (h) return h;
        }
        return null;
    }
}