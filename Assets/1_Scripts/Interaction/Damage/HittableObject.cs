using UnityEngine;

public abstract class HittableObject : MonoBehaviour, IDamageable
{
    LifeComponent life;

    [Header("HittableObject")]
    [SerializeField] int maxLife;

    void Awake()
    {
        life = new LifeComponent(maxLife, OnDeath);
        OnAwake();
    }

    void IDamageable.Damage(DamageData data)
    {
        if (life.Damage(data.damage, out bool death))
        {
            if (!death)
            {
                OnHit(data);
            }
        }
    }

    protected virtual void OnAwake()
    {

    }
    public abstract void OnHit(DamageData data);

    public abstract void OnDeath();
}
