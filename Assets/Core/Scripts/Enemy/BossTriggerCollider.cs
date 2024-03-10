using UnityEngine;

public class BossTriggerCollider : MonoBehaviour
{
    public HarelessBoss harelessBoss;

    private void Start()
    {
        harelessBoss.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            harelessBoss.enabled = true;
        }
    }
}
