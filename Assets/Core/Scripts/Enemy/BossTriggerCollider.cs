using UnityEngine;

public class BossTriggerCollider : MonoBehaviour
{
    public HarelessBoss harelessBoss;

    private void Start()
    {
        harelessBoss.enabled = false; // Ensure boss logic is initially disabled
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            harelessBoss.enabled = true; // Enable the boss script to start logic in Update
            harelessBoss.isActivated = true; // Activate the boss battle logic
        }
    }
}
