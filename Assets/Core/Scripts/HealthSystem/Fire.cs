using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    public string shooterTag; // Tag of the shooter

    private void Start()
    {
        Destroy(gameObject, 10f); // Auto-destroy after 10 seconds
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object isn't the shooter
        if (other.CompareTag(shooterTag) == false)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Destroy(gameObject); // Destroy the projectile after inflicting damage
            }
        }
    }
}
