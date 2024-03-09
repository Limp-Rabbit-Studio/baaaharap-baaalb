using UnityEngine;

public class EnemyShootingTemplate : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform player;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float shootingRange = 10f;
    [SerializeField] private float shootingInterval = 1f;
    [SerializeField] private float force = 10f;
    [SerializeField] private float ammoLifeTime = 5f;

    private float timeSinceLastShot = 0f;

    private void Update()
    {
        if (player == null) return;

        if (IsPlayerInRange())
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= shootingInterval)
            {
                Shoot();
                timeSinceLastShot = 0f;
            }
        }
    }

    private bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) <= shootingRange;
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        Vector3 direction = (player.position - projectileSpawnPoint.position).normalized;
        projectileRb.velocity = direction * force;
        Destroy(projectile, ammoLifeTime);
    }
}
