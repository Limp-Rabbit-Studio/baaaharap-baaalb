using System.Collections;
using UnityEngine;

public class SheepDashAttack : MonoBehaviour
{
    Animator animctrl;

    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;
    private CharacterController characterController;
    private float cooldownTimer;
    int isDashingHash;
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("SheepDashAttack: No CharacterController attached to the GameObject");
        }
    }

    private void Start()
    {
        animctrl = GetComponentInChildren<Animator>();
        isDashingHash = Animator.StringToHash("_isDashing");
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
        animctrl.SetBool(isDashingHash, true);
        float startTime = Time.time;
        Vector3 dashDirection = transform.forward * dashSpeed;

        while (Time.time < startTime + dashTime)
        {
            characterController.Move(dashDirection * Time.deltaTime);
            yield return null; // Wait for the next frame
        }
        animctrl.SetBool(isDashingHash, false);
        Debug.Log("Dash is done - SheepDashAttack");
    }
}
