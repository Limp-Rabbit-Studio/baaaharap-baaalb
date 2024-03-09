using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private HealthBar healthBar;

    public float currentHealth;

    private void Update()
    {
        Debug.Log("Enemy Health: " + currentHealth);
    }

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetSliderMax(maxHealth);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (healthBar != null)
        {
            healthBar.SetSlider(currentHealth);
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
         Destroy(gameObject);
        // gameObject.SetActive(false);
    }
}
