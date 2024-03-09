using System;
using UnityEngine;

public class DashCollisionHandler : MonoBehaviour
{
    public SheepDashAttack sheepDashAttackScript;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.name);
        Debug.Log("Dash Collision: Trigger entered.");
        // Respond only to objects tagged as "Enemy"
        Debug.Log("Is Dashing" + sheepDashAttackScript.IsDashing);
        if (other.CompareTag("Enemy") && sheepDashAttackScript.IsDashing)
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(sheepDashAttackScript.damage);
                Debug.Log("Dash Collision: Enemy hit with damage.");
            }
            else
            {
                Debug.LogWarning("Dash Collision: The object tagged as 'Enemy' does not have an EnemyStats component.");
            }
        }
        // There is no else clause needed here, as we want to ignore all other collisions
    }
}
