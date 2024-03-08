using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsSunday : MonoBehaviour
{
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasPlayer && Input.GetKeyDown(KeyCode.E))
        {
            MsSundayManager.Instance.ActivateHug(gameObject);
            PlayerUpgrade pu = hasPlayer.GetComponent<PlayerUpgrade>();
            //if (pu != null)
            {
                pu.Upgrade();
            }
            hasPlayer = null;
        }
    }

    GameObject hasPlayer = null;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasPlayer = other.gameObject;
            MsSundayManager.Instance.DisplayDialog();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasPlayer = null;
            MsSundayManager.Instance.HideDialog();
        }
    }
}
