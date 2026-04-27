using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected LayerMask layerMask;
    protected float damage = 50;
    protected Vector3 lastFramePosition;

    public LayerMask LayerMask => layerMask;

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
        Debug.DrawLine(lastFramePosition, transform.position, Color.red, 10);
        RaycastHit2D[] hits = Physics2D.LinecastAll(lastFramePosition, transform.position);
        // print("Hit count : " + hits.Length);
        if (hits.Length == 0) return;
        for (int i = 0; i < hits.Length; i++)
        {
            if (!hits[i].collider) continue;
            Damagable h = hits[i].collider.GetComponent<Damagable>();
            if (h)
            {
                h.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
