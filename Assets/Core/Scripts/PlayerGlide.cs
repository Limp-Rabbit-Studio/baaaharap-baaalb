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

    void Awake()
    {
        controller = GetComponent<ThirdPersonController>();
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
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool(isGlidingHash, false);
            controller.Gravity = -15;
        }
    }
}
