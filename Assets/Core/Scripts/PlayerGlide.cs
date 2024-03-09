using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlide : MonoBehaviour
{
    Animator animator;
    ThirdPersonController controller;
    int isGlidingHash;
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
        isGlidingHash = Animator.StringToHash("_isGliding");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool(isGlidingHash, true);
            controller.Gravity = -8;
            PlayGlideSound();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool(isGlidingHash, false);
            controller.Gravity = -15;
            StopGlideSound();
        }
    }

    void PlayGlideSound()
    {
        AudioSource.PlayClipAtPoint(glideAudioClip, transform.position, glideAudioVolume);
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
