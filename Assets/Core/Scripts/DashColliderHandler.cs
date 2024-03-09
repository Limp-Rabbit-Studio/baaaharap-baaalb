using System;
using UnityEngine;

public class DashCollisionHandler : MonoBehaviour
{
    public SheepDashAttack sheepDashAttackScript;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.name + "    " + other.tag + "     " + sheepDashAttackScript.IsDashing + "   " + other.GetComponent<EnemyStats>());
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
