using UnityEngine;

public class RabbitEnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
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

    public Animator AIanim;
    int AIisJumpingHash;
    float actualJumpInterval;

    [SerializeField] private Transform player;

    private void Start()
    {
        AIanim  = GetComponent<Animator>();
        float scaleRand = Random.Range(-.1f, .1f);
        transform.localScale = Vector3.one + new Vector3(scaleRand, scaleRand, scaleRand);
        AIanim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        transform.position = spawnPoint.position;
        AIisJumpingHash = Animator.StringToHash("AI_isJumping");
        actualJumpInterval = jumpInterval + Random.Range(-.2f, .2f);
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

        transform.LookAt(player);
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
        if (timeSinceLastJump >= actualJumpInterval && rb != null)
        {
            AIanim.SetBool(AIisJumpingHash, true);
            DoSimpleJump();
            // DoComplexJump();
            AIanim.SetBool(AIisJumpingHash, false);

            actualJumpInterval = jumpInterval + Random.Range(-.2f, .2f);
            timeSinceLastJump = 0f;
        }
    }

    [SerializeField] float distanceX;
    [SerializeField] float distanceZ;
    [SerializeField] Vector3 crtLoc;
    [SerializeField] Vector3 newLoc;
    [SerializeField] Vector3 delta;
    void DoSimpleJump()
    {
        distanceX = Random.Range(-jumpRadius, jumpRadius);
        distanceZ = Random.Range(-jumpRadius, jumpRadius);
        crtLoc = transform.position;
        newLoc = new Vector3(
           distanceX + spawnPoint.position.x,
           0,
           distanceZ + spawnPoint.position.z);
        delta = newLoc - crtLoc;
        delta.y = jumpForce;
        rb.AddForce(delta * 1, ForceMode.Impulse);
    }

    void DoComplexJump()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Mathf.Atan2(directionToPlayer.z, directionToPlayer.x) * Mathf.Rad2Deg;
        angleToPlayer = (angleToPlayer + 360) % 360;
        int playerDivision = (int)(angleToPlayer / 90);

        // Choose a random division to jump towards
        int targetDivision = Random.Range(0, 4);

        // Avoid obstacles in the chosen division
        if (divisionJumpCount[playerDivision] >= divisionJumpCount[targetDivision])
        {
            targetDivision = (targetDivision + 2) % 4; // Jump to the opposite side
        }

        float jumpAngle = targetDivision * 90f + Random.Range(0, 90f);
        Vector2 jumpDirection = new Vector2(Mathf.Cos(jumpAngle * Mathf.Deg2Rad), Mathf.Sin(jumpAngle * Mathf.Deg2Rad));
        Vector3 jumpVector = new Vector3(jumpDirection.x, 0, jumpDirection.y) * jumpRadius;

        // Calculate the potential new position after the jump
        Vector3 potentialNewPosition = transform.position + new Vector3(jumpVector.x * jumpForce, 0, jumpVector.z * jumpForce);

        // Check if the new position would be within the allowed radius from the spawn point
        if (Vector3.Distance(spawnPoint.position, potentialNewPosition) <= jumpRadius)
        {
            rb.AddForce(new Vector3(jumpVector.x, jumpForce, jumpVector.z), ForceMode.Impulse);
            divisionJumpCount[targetDivision]++; // Increment the jump count for the chosen division
        }
    }
}
