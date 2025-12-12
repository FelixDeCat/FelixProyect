using System;

public class LifeComponent
{
    int life = 100;
    int maxLife = 100;
    bool isDead = false;
    Action _onDeath;

    public LifeComponent(int initialLife, Action OnDeath)
    {
        life = initialLife;
        maxLife = initialLife;
        isDead = false;
        _onDeath = OnDeath;
    }

    public bool Damage(int quant, out bool death)
    {
        death = false;
        if (isDead) return false;

        life -= quant;
        if (life <= 0)
        {
            life = 0;
            isDead = true;
            death = true;
            _onDeath?.Invoke();
        }
        return true;
    }

    public bool Heal(int quant)
    {
        if (isDead) return false;

        if (life >= maxLife)
        {
            life = maxLife;
            return false;
        }

        life += quant;
        if (life >= maxLife)
        {
            life = maxLife;
        }

        return true;
    }

    public float Get01Percent()
    {
        return (float)life / maxLife;
    }
}
