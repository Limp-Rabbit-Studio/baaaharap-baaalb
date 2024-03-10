using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private HealthBar healthBar;
    private Animator animctrl; // From the first script
    private float currentHealth;
    public bool isDead = false;
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetSliderMax(maxHealth);
        animctrl = GetComponentInChildren<Animator>(); // From the first script
    }

    private void Update()
    {
        // DO NOT ENSURE HEALTH BOUNDS - DEBUG REASONS
        // EnsureHealthBounds();
        if(Input.GetKeyDown(KeyCode.H))
        {
            currentHealth += 100;
            healthBar.SetSlider(currentHealth);
        }
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
            animctrl.SetBool("_isDead", true);
        }
        isDead = true;
    }

}
