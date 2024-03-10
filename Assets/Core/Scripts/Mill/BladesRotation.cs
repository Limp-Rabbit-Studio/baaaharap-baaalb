using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladesRotation : MonoBehaviour
{
    public float speed = 1f;
    public GameObject rotationObj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rotationObj.transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
