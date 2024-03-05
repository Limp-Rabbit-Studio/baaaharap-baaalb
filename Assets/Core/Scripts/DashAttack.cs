using System.Collections;
using UnityEngine;

public class SheepDashAttack : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;

    private CharacterController characterController;
    private float cooldownTimer;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("SheepDashAttack: No CharacterController attached to the GameObject");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0f && characterController != null)
        {
            StartCoroutine(DoDash());
            cooldownTimer = dashCooldown;
        }

        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    IEnumerator DoDash()
    {
        float startTime = Time.time;
        Vector3 dashDirection = transform.forward * dashSpeed;

        while (Time.time < startTime + dashTime)
        {
            characterController.Move(dashDirection * Time.deltaTime);
            yield return null; // Wait for the next frame
        }
    }
}
