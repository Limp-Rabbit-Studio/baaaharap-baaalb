using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    public string shooterTag;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("PlayerDash")) && !other.CompareTag(shooterTag))
        {
            IDamageable damageable = other.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
