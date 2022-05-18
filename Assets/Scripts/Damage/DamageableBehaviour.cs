using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableBehaviour : MonoBehaviour, IDamageable {
    [SerializeField]
    [Min(1)]
    private int maxHealth = 10;

    [SerializeField]
    [Min(0)]
    private float invulnerabilityWindow = 2;

    public event Action<DamageEventParams> DamageEvent;
    public event Action DeathEvent;

    public int MaxHealth => maxHealth;

    public int CurrentHealth { get; private set; }

    public bool IsDead => CurrentHealth == 0;

    private bool CanTakeDamage => !IsDead && Time.time >= invulnerabilityExpireTime;

    private float invulnerabilityExpireTime;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (CanTakeDamage)
        {
            var previousHealth = CurrentHealth;
            CurrentHealth = Mathf.Max(0, CurrentHealth - damageAmount);
            DamageEvent?.Invoke(new DamageEventParams(previousHealth, CurrentHealth));
            if (IsDead)
            {
                DeathEvent?.Invoke();
            }
            invulnerabilityExpireTime = Time.time + invulnerabilityWindow;
        }
    }
}
