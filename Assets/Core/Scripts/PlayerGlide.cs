using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlide : MonoBehaviour
{
    ThirdPersonController controller;

    void Awake()
    {
        controller = GetComponent<ThirdPersonController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.Gravity = -8;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            controller.Gravity = -15;
        }
    }
}
