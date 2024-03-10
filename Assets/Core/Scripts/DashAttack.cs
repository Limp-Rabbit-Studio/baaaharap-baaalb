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
    private CharacterController characterController;
    private float cooldownTimer;
    private bool isDashing = false;

    public bool IsDashing
    {
        get { return isDashing; }
    }

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        signifier.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0f && characterController != null)
        {
            StartCoroutine(DoDash());
            cooldownTimer = dashCooldown;
            signifier.SetActive(false);
            AudioSource.PlayClipAtPoint(swooshSound, transform.position);
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
    }
}
