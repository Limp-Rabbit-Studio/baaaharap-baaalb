using UnityEngine;
using System.Collections;

public class HarelessBoss : MonoBehaviour
{
    public Transform[] movePositions; // Assume at least 4 positions are set in the Inspector
    public GameObject bulletPrefab;
    public float dashSpeed = 30f; // Speed of dash
    public float stayDuration = 5f; // How long to stay in a position
    public float positionChangeInterval = 4f; // Normal movement interval
    public float hitDashDelay = 1f; // Delay between dashes when hit
    public Rigidbody rb; // Assuming this is set via the Inspector or Start method

    public Transform projectileSpawnPoint; // Set this in the Inspector
    public GameObject projectilePrefab; // Set this in the Inspector
    public float shootingInterval = 2f; // Time between each shot sequence
    public float projectileForce = 20f; // Speed of the projectile
    public float shotSpacing = 0.5f; // Time between individual shots in a sequence
    private float shootingTimer;
    public Animator enemyanim;


    private float positionChangeTimer;
    public bool isActivated = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        positionChangeTimer = positionChangeInterval;
        shootingTimer = shootingInterval;
        enemyanim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isActivated) return;

        positionChangeTimer -= Time.deltaTime;
        if (positionChangeTimer <= 0)
        {
            DashToRandomPosition();
            positionChangeTimer = positionChangeInterval + stayDuration;
        }
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0)
        {
            StartCoroutine(ShootSequence());
            shootingTimer = shootingInterval;
        }
    }
    IEnumerator ShootSequence()
    {
        for (int i = 0; i < 3; i++)
        {
            ShootProjectile();
            yield return new WaitForSeconds(shotSpacing);
        }
    }
    void ShootProjectile()
    {
        enemyanim.SetTrigger("Span_Atac");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player && projectilePrefab && projectileSpawnPoint)
        {
            Vector3 direction = (player.transform.position - projectileSpawnPoint.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.LookRotation(direction));
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            projectileRb.velocity = direction * projectileForce;

            Destroy(projectile, 5f);
        }
    }


    void DashToRandomPosition()
    {
        int index = Random.Range(0, movePositions.Length);
        StartCoroutine(DashToPosition(movePositions[index].position));
    }

    IEnumerator DashToPosition(Vector3 targetPosition)
    {
        float timeToDash = Vector3.Distance(transform.position, targetPosition) / dashSpeed;
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < timeToDash)
        {
            rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, elapsedTime / timeToDash));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.position = targetPosition;
    }

    public void OnHit()
    {
        StartCoroutine(PerformHitReactionDashes());
    }

    IEnumerator PerformHitReactionDashes()
    {
        for (int i = 0; i < 3; i++) // Perform 3 dashes
        {
            int newIndex = Random.Range(0, movePositions.Length);
            yield return DashToPosition(movePositions[newIndex].position);
            yield return new WaitForSeconds(hitDashDelay); // Wait before the next dash
        }
    }
}
