using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    Animator AIanimation;
    int AIisDeadHash;
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private HealthBar healthBar;

    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private GameObject vfxDeath;
    public Animator enemyanim;
    public float currentHealth;

    private void Start()
    {
        AIanimation = GetComponent<Animator>();
        AIisDeadHash = Animator.StringToHash("AI_isDead");
        enemyanim = GetComponent<Animator>();
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
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            BossTag bossTag = GetComponent<BossTag>();
            if (bossTag != null)
            {
                HarelessBoss harelessBoss = GetComponent<HarelessBoss>();
                if (harelessBoss != null)
                {
                    enemyanim.SetTrigger("Span_Damage");
                    harelessBoss.OnHit();
                }
            }
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
        // null check for AIanimation
        if (AIanimation != null)
        {
            AIanimation.SetBool(AIisDeadHash, true);
        }
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }
        GameObject go = Instantiate(vfxDeath);
        go.transform.position = transform.position;
        Destroy(go, .8f);
        Destroy(gameObject);
        // gameObject.SetActive(false);
    }
}
