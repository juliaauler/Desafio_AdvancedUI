using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {
    [SerializeField] public Image healthBar;
    [SerializeField] public DamageableBehaviour damageable;
    [SerializeField] [Min(0.1f)] public float speed = 2f;
    
    private void LateUpdate() {
        float healthPercent = (float) damageable.CurrentHealth / damageable.MaxHealth;
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, healthPercent, Time.deltaTime * speed);
    }
}