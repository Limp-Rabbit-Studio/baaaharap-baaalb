using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private HealthBar healthBar;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetSliderMax(maxHealth);
    }

    private void Update()
    {
        EnsureHealthBounds();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetSlider(currentHealth);
        CheckDeath();
    }

    public void HealPlayer(float amount)
    {
        currentHealth += amount;
        EnsureHealthBounds();
        healthBar.SetSlider(currentHealth);
    }

    private void EnsureHealthBounds()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        // Handle death (animations, game over screen, etc.)
    }
}
