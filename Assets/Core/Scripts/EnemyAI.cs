using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform player;
    public Transform projectileSpawnPoint;
    public float shootingRange = 10f;
    public float shootingInterval = 1f;
    public float force = 10f;
    public float ammoLifeTime = 5f;
    private float timeSinceLastShot = 0f;

    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) <= shootingRange)
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= shootingInterval)
            {
                Shoot();
                timeSinceLastShot = 0f;
            }
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 direction = (player.position - projectileSpawnPoint.position).normalized;
        projectileRb.velocity = direction * force;

        //So we don't fill the mapp with fire balls, after 'nf' time they just poof
        Destroy(projectile, ammoLifeTime);
    }
}
