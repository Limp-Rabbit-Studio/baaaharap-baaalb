using UnityEngine;

public class RabbitEnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform player;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float shootingRange = 10f;
    [SerializeField] private float shootingInterval = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpInterval = 2f;
    [SerializeField] private float jumpRadius = 5f;
    [SerializeField] private float projectileForce = 10f;
    [SerializeField] private float projectileLifetime = 5f;

    private float timeSinceLastShot = 0f;
    private float timeSinceLastJump = 0f;
    private Rigidbody rb;
    private int[] divisionJumpCount = new int[4];
    private int lastJumpDivision = -1;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = spawnPoint.position;
    }

    private void Update()
    {
        if (player == null) return;

        JumpRandomlyTowardsPlayer();

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
        projectileRb.velocity = direction * projectileForce;
        Destroy(projectile, projectileLifetime);
    }

    private void JumpRandomlyTowardsPlayer()
    {
        timeSinceLastJump += Time.deltaTime;
        if (timeSinceLastJump >= jumpInterval && rb != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            float angleToPlayer = Mathf.Atan2(directionToPlayer.z, directionToPlayer.x) * Mathf.Rad2Deg;
            angleToPlayer = (angleToPlayer + 360) % 360;
            int playerDivision = (int)(angleToPlayer / 90);
            int adjacentDivision = (playerDivision + 1) % 4;
            int targetDivision = Random.Range(0, 2) == 0 ? playerDivision : adjacentDivision;
            float jumpAngle = targetDivision * 90f + Random.Range(0, 90f);
            Vector2 jumpDirection = new Vector2(Mathf.Cos(jumpAngle * Mathf.Deg2Rad), Mathf.Sin(jumpAngle * Mathf.Deg2Rad));
            Vector3 jumpVector = new Vector3(jumpDirection.x, 0, jumpDirection.y) * jumpRadius;

            rb.AddForce(new Vector3(jumpVector.x, jumpForce, jumpVector.z), ForceMode.Impulse);
            timeSinceLastJump = 0f;

            for (int i = 0; i < divisionJumpCount.Length; i++)
            {
                divisionJumpCount[i] = i == targetDivision ? 1 : 0;
            }
            lastJumpDivision = targetDivision;
        }
    }
}
