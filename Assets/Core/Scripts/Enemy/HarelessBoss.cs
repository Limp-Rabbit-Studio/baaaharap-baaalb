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

    private float positionChangeTimer;
    public bool isActivated = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        positionChangeTimer = positionChangeInterval;
    }

    void Update()
    {
        if (!isActivated) return;

        positionChangeTimer -= Time.deltaTime;
        if (positionChangeTimer <= 0)
        {
            DashToRandomPosition();
            positionChangeTimer = positionChangeInterval + stayDuration; // Reset timer to include stay duration
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

        rb.position = targetPosition; // Ensure the boss reaches the target position
    }

    // Call this method when the boss is hit
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
