using UnityEngine;
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    public HealthBar healthBar;

    private float currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetSliderMax(maxHealth);
    }
    private void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetSlider(currentHealth);
    }
    public void HealPlayer(float amount)
    {
        currentHealth += amount;
        healthBar.SetSlider(currentHealth);
    }
    private void Die()
    {
        Debug.Log("Te-a ars bai baahter!");

        // Animatie Moarte

        // Ecran Moarte

        //...
    }
}