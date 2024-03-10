using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladesPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Quaternion qa = transform.rotation;
        transform.rotation = Quaternion.Euler(qa.x, qa.y, 0);
    }
}
