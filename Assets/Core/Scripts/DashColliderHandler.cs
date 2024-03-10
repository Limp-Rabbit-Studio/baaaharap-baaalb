using UnityEngine;

public class DashCollisionHandler : MonoBehaviour
{
    public SheepDashAttack sheepDashAttackScript;
    public AudioClip bangSound;

    private void OnTriggerEnter(Collider other)
    {
        if (!sheepDashAttackScript.IsDashing)
        {
            return;
        }
        if (!other.CompareTag("Player"))
        {
            sheepDashAttackScript.StopDash();
        }
        if (other.CompareTag("Hareless"))
        {
            HarelessBoss hb = other.GetComponent<HarelessBoss>();
            if (hb != null)
            {
                sheepDashAttackScript.ResetCooldown();
                hb.Hit();
                audioSource.PlayOneShot(bangSound);
            }
        }
        // Debug.Log("Collided with: " + other.name + "    " + other.tag + "     " + sheepDashAttackScript.IsDashing + "   " + other.GetComponent<EnemyStats>());
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                sheepDashAttackScript.ResetCooldown();
                enemy.TakeDamage(sheepDashAttackScript.damage);
                // Debug.Log("Dash Collision: Enemy hit with damage.");

                // Play bang sound
                audioSource.PlayOneShot(bangSound);
            }
            else
            {
                // Debug.LogWarning("Dash Collision: The object tagged as 'Enemy' does not have an EnemyStats component.");
            }
            AudioSource.PlayClipAtPoint(bangSound, transform.position);
        }
    }
}
