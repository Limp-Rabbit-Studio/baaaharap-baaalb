using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDoors : MonoBehaviour
{
    [SerializeField] GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            for (int i = 0; i < doors.Length; i++)
            {
                Destroy(doors[i]);
            }
        }
    }
}
