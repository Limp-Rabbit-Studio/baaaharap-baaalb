using System.Collections;
using UnityEngine;

public class SheepDashAttack : MonoBehaviour
{
    public GameObject signifier;
    public Animator anim;
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;
    public float damage = 10f;
    public AudioClip swooshSound;
    public AudioClip bangSound;
    private CharacterController characterController;
    private float cooldownTimer;
    private bool isDashing = false;
    private AudioSource audioSource;

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
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        if (anim == null)
        {
            // Debug.LogError("SheepDashAttack: No Animator found in children.");
        }
        else
        {
            // Debug.Log("SheepDashAttack: Animator component found.");
        }
        signifier.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0f && characterController != null)
        {
            // Debug.Log("SheepDashAttack: Dash initiated.");
            StartCoroutine(DoDash());
            cooldownTimer = dashCooldown;
            signifier.SetActive(false);

            // Play swoosh sound
            audioSource.PlayOneShot(swooshSound);
        }

        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                signifier.SetActive(true);
            }
        }
    }

    IEnumerator DoDash()
    {
        // Debug.Log("SheepDashAttack: Starting dash.");
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
        // Debug.Log("SheepDashAttack: Dash completed.");

        // Additional log to confirm dash completion and reset
        if (!isDashing)
        {
            // Debug.Log("SheepDashAttack: No longer dashing.");
        }
    }
}
