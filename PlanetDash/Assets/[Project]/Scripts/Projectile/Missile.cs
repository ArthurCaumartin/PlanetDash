using UnityEngine;

public class Missile : Projectile
{
    protected Health target;
    protected float speed = 10;
    protected float trakingSpeed = 3;
    protected float explosionRange = 3;


    public virtual Projectile Init(Health target,
                                   float damage,
                                   float speed,
                                   float trakingSpeed,
                                   float explosionRange)
    {
        this.target = target;
        this.damage = damage;
        this.speed = speed;
        this.trakingSpeed = trakingSpeed;
        this.explosionRange = explosionRange;

        lastFramePosition = transform.position;

        return this;
    }

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

        Vector3 dirToTarget = target.transform.position - transform.position;
        dirToTarget = dirToTarget.normalized;

        float angle = Vector3.SignedAngle(transform.up, dirToTarget, Vector3.forward);
        transform.Rotate(Vector3.forward, Time.deltaTime * trakingSpeed * Mathf.Sign(angle));
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        TryDetectDamagable();
    }

    private Health TryDetectNewTarget()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 10);
        if (cols.Length == 0) return null;
        for (int i = 0; i < cols.Length; i++)
        {
            Health h = cols[i].GetComponent<Health>();
            return h;
        }
        return null;
    }
}