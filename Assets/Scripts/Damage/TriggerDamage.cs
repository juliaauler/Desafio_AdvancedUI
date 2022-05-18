using UnityEngine;
using UnityEngine.Assertions;

public class TriggerDamage : MonoBehaviour {
    private CustomBullet _cb; 
    [SerializeField] [Min(0)] public int damage = 1;
    
    private void Awake()
    {
        var thisCollider = GetComponent<Collider>();
        _cb = GetComponent<CustomBullet>();
        Assert.IsNotNull(thisCollider);
        Assert.IsTrue(thisCollider.isTrigger, "Collider must me marked as trigger");
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            _cb.Explode();
        }
    }
}
