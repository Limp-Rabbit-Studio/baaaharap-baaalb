using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    Animator AIanimation;
    int AIisDeadHash;
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private HealthBar healthBar;

    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;

    public float currentHealth;

    private void Start()
    {
        AIanimation = GetComponent<Animator>();
        AIisDeadHash = Animator.StringToHash("AI_isDead");

        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetSliderMax(maxHealth);
        }
    }

    public void TakeDamage(float amount)
    {
        Debug.Log("EnemyStats: TakeDamage called with amount: " + amount);
        currentHealth -= amount;
        if (healthBar != null)
        {
            healthBar.SetSlider(currentHealth);
        }
        if (damageSound != null)
        {
            AudioSource.PlayClipAtPoint(damageSound, transform.position);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (healthBar != null)
        {
            healthBar.SetSlider(currentHealth);
        }
    }

    private void Die()
    {
        AIanimation.SetBool(AIisDeadHash, true);
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }
        Destroy(gameObject);
        // gameObject.SetActive(false);
    }
}
