using UnityEngine;

public class DashCollisionHandler : MonoBehaviour
{
    public SheepDashAttack sheepDashAttackScript;

    private void OnTriggerEnter(Collider other)
    {
        if (sheepDashAttackScript.IsDashing && other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(sheepDashAttackScript.damage);
                Debug.Log("Enemy hit by dash.");
            }
        }
    }
}
