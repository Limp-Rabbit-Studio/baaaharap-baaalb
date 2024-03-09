using System.Collections;
using UnityEngine;

public class SheepDashAttack : MonoBehaviour
{
    public Animator anim;
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;
    public float damage = 10f;
    private CharacterController characterController;
    private float cooldownTimer;
    private bool isDashing = false;

    // Make the isDashing property public so it can be accessed from the DashCollisionHandler
    public bool IsDashing
    {
        get { return isDashing; }
    }

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
        anim = GetComponentInChildren<Animator>();
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
        anim.SetBool("_isDashing", true);
        isDashing = true;

        float startTime = Time.time;
        Vector3 dashDirection = transform.forward * dashSpeed;

        while (Time.time < startTime + dashTime)
        {
            characterController.Move(dashDirection * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
        anim.SetBool("_isDashing", false);
        Debug.Log("Dash is done - SheepDashAttack");
    }
}
