using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected LayerMask layerMask;
    protected float damage = 50;
    protected Vector3 lastFramePosition;

    protected virtual void Start() { }
    protected virtual void Update() { }
    protected virtual void LateUpdate()
    {
        lastFramePosition = transform.position;
    }

    public virtual Projectile Init(float damage)
    {
        this.damage = damage;
        lastFramePosition = transform.position;
        return this;
    }

    protected void TryDetectDamagable()
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(lastFramePosition, transform.position);
        print("Hit count : " + hits.Length);
        if (hits.Length == 0) return;
        for (int i = 0; i < hits.Length; i++)
        {
            if (!hits[i].collider) continue;
            Health h = hits[i].collider.GetComponent<Health>();
            h?.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
