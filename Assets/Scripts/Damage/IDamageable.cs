using System;

public readonly struct DamageEventParams
{
    public readonly int PreviousHealth;
    public readonly int CurrentHealth;

    public DamageEventParams(int previousHealth, int currentHealth)
    {
        PreviousHealth = previousHealth;
        CurrentHealth = currentHealth;
    }
}

public interface IDamageable
{
    int MaxHealth { get; }
    int CurrentHealth { get; }
    bool IsDead { get; }

    void TakeDamage(int damageAmount);

    event Action<DamageEventParams> DamageEvent;
    event Action DeathEvent;
}