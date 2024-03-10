using System.Collections;
using UnityEngine;

public class SheepDashAttack : MonoBehaviour
{
    public Animator anim;
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;
    public float damage = 10f;
    public AudioClip swooshSound;
    private CharacterController characterController;
    private float cooldownTimer;
    private bool isDashing = false;
    public GameObject vfx;
    private AudioSource audioSource;

    public bool IsDashing
    {
        get { return isDashing; }
    }

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            // Debug.LogError("SheepDashAttack: No CharacterController attached to the GameObject");
        }
        else
        {
            // Debug.Log("SheepDashAttack: CharacterController found.");
        }
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Debug.LogError("SheepDashAttack: No AudioSource found. Please attach an AudioSource to the GameObject.");
        }
        anim = GetComponentInChildren<Animator>();
        if (anim == null)
        {
            // Debug.LogError("SheepDashAttack: No Animator found in children.");
        }
        else
        {
            // Debug.Log("SheepDashAttack: Animator component found.");
        }
        vfx.SetActive(false);
        cooldownTimer = dashCooldown;
    }

    private void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0f && characterController != null)
        {
            // Debug.Log("SheepDashAttack: Dash initiated.");
            StartCoroutine(DoDash());
            cooldownTimer = dashCooldown;
            vfx.SetActive(false);
            AudioSource.PlayClipAtPoint(swooshSound, transform.position);
        }

        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                vfx.SetActive(true);
            }
        }
    }

    public void ResetCooldown()
    {
        cooldownTimer = 0f;
        vfx.SetActive(true);
    }

    public void StopDash()
    {
        forceStop = true;
    }

    bool forceStop = false;

    IEnumerator DoDash()
    {
        // Debug.Log("SheepDashAttack: Starting dash.");
        anim.SetBool("_isDashing", true);
        isDashing = true;

        float startTime = Time.time;
        Vector3 dashDirection = transform.forward * dashSpeed;

        forceStop = false;

        while (Time.time < startTime + dashTime)
        {
            if (forceStop)
            {
                forceStop = false;
                break;
            }
            characterController.Move(dashDirection * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
        anim.SetBool("_isDashing", false);
        // Debug.Log("SheepDashAttack: Dash completed.");

        // Additional log to confirm dash completion and reset
        if (!isDashing)
        {
            // Debug.Log("SheepDashAttack: No longer dashing.");
        }
    }
}
