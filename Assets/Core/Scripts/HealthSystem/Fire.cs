using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    private void Start()
    {
        Destroy(gameObject, 10f); // Auto-destroy after 10 seconds
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            Destroy(gameObject); // Destroy the projectile after inflicting damage
        }
    }
}
