using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlide : MonoBehaviour
{
    Animator animator;
    ThirdPersonController controller;
    float airTime;

    [SerializeField] private AudioClip glideAudioClip;
    [SerializeField] private float glideAudioVolume = 0.7f;

    private AudioSource glideAudioSource; // To keep track of the playing sound

    void Awake()
    {
        controller = GetComponent<ThirdPersonController>();
        glideAudioSource = GetComponent<AudioSource>(); // Store in temp variable
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("_isGliding", true);
            animator.SetBool("_isGrounded", false);
            animator.SetBool("_isJumping", true);
            controller.Gravity = -8;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            controller.Gravity = -15;
        }

        if (animator.GetBool("_isGrounded") == true)
        {
            animator.SetBool("_isJumping", false);
            animator.SetBool("_isGliding", false);
        }
    }

    void PlayGlideSound()
    {
        // AudioSource.PlayClipAtPoint(glideAudioClip, transform.position, glideAudioVolume);
    }

    void StopGlideSound()
    {
        if (glideAudioSource != null)
        {
            glideAudioSource.Stop();
            glideAudioSource = null;
        }
    }
}
