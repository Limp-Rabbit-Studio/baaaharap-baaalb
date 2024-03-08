using UnityEngine;

public class HalfManHalfRabbitAI : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float shootingRange = 10f;
    [SerializeField] private float minimumSafeDistance = 5f;
    [SerializeField] private float shootingInterval = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float bigJumpForce = 15f; // New
    [SerializeField] private float jumpCooldown = 2f;
    [SerializeField] private float bigJumpCooldown = 30f; // New
    [SerializeField] private float detectionRange = 15f;
    [SerializeField] private float fieldOfView = 110f;
    [SerializeField] private LayerMask whatIsObstacle;
    [SerializeField] private float force = 10f;
    [SerializeField] private float ammoLifeTime = 5f;
    [SerializeField] private float healthThresholdForBigJump = 20f; // New

    private Transform player;
    private Rigidbody rb;
    private float timeSinceLastShot = 0f;
    private float timeSinceLastJump = 0f;
    private float timeSinceLastBigJump = Mathf.Infinity; // New

    private EnemyStats enemyStats;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        enemyStats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        bool isPlayerDetected = DetectPlayer();

        // New - Checking if big jump is needed
        if (enemyStats.currentHealth <= healthThresholdForBigJump && timeSinceLastBigJump >= bigJumpCooldown)
        {
            PerformBigJumpRetreat();
            timeSinceLastBigJump = 0f;
        }
        else
        {
            if (isPlayerDetected)
            {
                HandleCombat();
            }
            else
            {
                IdleJump();
            }
        }

        timeSinceLastShot += Time.deltaTime;
        timeSinceLastJump += Time.deltaTime;
        timeSinceLastBigJump += Time.deltaTime; // New
    }


    private bool DetectPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        if (directionToPlayer.magnitude <= detectionRange)
        {
            float angle = Vector3.Angle(transform.forward, directionToPlayer.normalized);
            if (angle <= fieldOfView * 0.5f)
            {
                RaycastHit hit;
                if (!Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, detectionRange, whatIsObstacle))
                {
                    return true;
                }
            }
        }
        return false;
    }
    private void PerformBigJumpRetreat()
    {
        Vector3 retreatDirection = (transform.position - player.position).normalized + Vector3.up * 0.5f;
        rb.AddForce(retreatDirection.normalized * bigJumpForce, ForceMode.Impulse);
    }
    private void HandleCombat()
    {
        timeSinceLastShot += Time.deltaTime;
        timeSinceLastJump += Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > shootingRange)
        {
            if (timeSinceLastJump >= jumpCooldown)
            {
                JumpTowardsPlayer();
                timeSinceLastJump = 0f;
            }
        }
        else if (distanceToPlayer < minimumSafeDistance)
        {
            if (timeSinceLastJump >= jumpCooldown)
            {
                RetreatFromPlayer();
                timeSinceLastJump = 0f;
            }
            if (timeSinceLastShot >= shootingInterval)
            {
                Shoot();
                timeSinceLastShot = 0f;
            }
        }
        else
        {
            if (timeSinceLastShot >= shootingInterval)
            {
                Shoot();
                timeSinceLastShot = 0f;
            }
        }
    }

    private void JumpTowardsPlayer()
    {
        Vector3 jumpDirection = (player.position - transform.position).normalized + Vector3.up;
        rb.AddForce(jumpDirection.normalized * jumpForce, ForceMode.Impulse);
    }

    private void RetreatFromPlayer()
    {
        Vector3 jumpDirection = (transform.position - player.position).normalized + Vector3.up;
        rb.AddForce(jumpDirection.normalized * jumpForce, ForceMode.Impulse);
    }

    private void Shoot()
    {   
        Debug.Log("Shooting");
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        Vector3 direction = (player.position - projectileSpawnPoint.position).normalized;
        projectileRb.velocity = direction * force;
        Destroy(projectile, ammoLifeTime);
    }

    private void IdleJump()
    {
        timeSinceLastJump += Time.deltaTime;
        if (timeSinceLastJump >= jumpCooldown)
        {
            Vector3 randomDirection = Random.insideUnitSphere;
            randomDirection.y = Mathf.Abs(randomDirection.y);
            rb.AddForce(randomDirection.normalized * jumpForce, ForceMode.Impulse);
            timeSinceLastJump = 0f;
        }
    }
}
