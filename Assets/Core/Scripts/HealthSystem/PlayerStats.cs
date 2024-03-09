using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private HealthBar healthBar;
    private Animator animctrl; // From the first script
    private int isDeadHash; // From the first script
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetSliderMax(maxHealth);
        animctrl = GetComponentInChildren<Animator>(); // From the first script
        isDeadHash = Animator.StringToHash("_isDead"); // From the first script
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
        Debug.Log("Player has died."); // Using the log from the first script
        if (animctrl != null) // Added a null-check for the Animator
        {
            animctrl.SetBool(isDeadHash, true);
        }
        // Handle death (choose better animation, game over screen, etc.)
    }
}
