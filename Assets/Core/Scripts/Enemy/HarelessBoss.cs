using UnityEngine;

public class HarelessBoss : MonoBehaviour
{
    public Transform[] movePositions;
    public GameObject bulletPrefab;
    public int bulletsToShoot = 3;
    public float shootInterval = 2f;
    public float moveSpeed = 5f;
    public float positionChangeInterval = 4f;

    private EnemyStats enemyStats;
    private float shootTimer;
    private float positionChangeTimer;
    private bool isActivated = false;

    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        shootTimer = shootInterval;
        positionChangeTimer = positionChangeInterval;
    }

    void Update()
    {
        if (!isActivated || enemyStats.currentHealth <= 0) return;

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            ShootBullets();
            shootTimer = shootInterval;
        }

        positionChangeTimer -= Time.deltaTime;
        if (positionChangeTimer <= 0)
        {
            ChangePosition();
            positionChangeTimer = positionChangeInterval;
        }
    }

    private void ChangePosition()
    {
        int index = Random.Range(0, movePositions.Length);
        transform.position = movePositions[index].position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerTrigger"))
        {
            isActivated = true;
        }
        else if (other.CompareTag("PlayerDash") && isActivated)
        {
            ChangePosition();
        }
    }

    private void ShootBullets()
    {
        for (int i = 0; i < bulletsToShoot; i++)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }
}
