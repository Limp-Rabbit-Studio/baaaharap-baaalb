using UnityEngine;

public class DashCollisionHandler : MonoBehaviour
{
    public SheepDashAttack sheepDashAttackScript;
    public AudioClip bangSound;

    private void OnTriggerEnter(Collider other)
    {
        if (sheepDashAttackScript.IsDashing)
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(sheepDashAttackScript.damage);
            }
            AudioSource.PlayClipAtPoint(bangSound, transform.position);
        }
    }
}
