using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTeleport : MonoBehaviour
{
    public GameObject teleportLocation;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && teleportLocation != null)
        {
            // Debug.Log("press T for teleportn");
            if (player != null)
            {
                // Debug.Log("tele mow");
                player.transform.position = teleportLocation.transform.position;
            }
        }
    }
}
