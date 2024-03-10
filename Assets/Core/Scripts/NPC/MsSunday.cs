using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsSunday : MonoBehaviour
{
    MsSundayManager msManager;
    [SerializeField] private Transform player;

    private void Awake()
    {
        msManager = GameObject.FindGameObjectWithTag("MsSundayManager").GetComponent<MsSundayManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPlayer && Input.GetKeyDown(KeyCode.E))
        {
            msManager.ActivateHug(gameObject);
            PlayerUpgrade pu = hasPlayer.GetComponent<PlayerUpgrade>();
            //if (pu != null)
            {
                pu.Upgrade();
            }
            hasPlayer = null;
        }
        Vector3 targetPosition = player.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);
    }

    GameObject hasPlayer = null;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasPlayer = other.gameObject;
            msManager.DisplayDialog();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasPlayer = null;
            msManager.HideDialog();
        }
    }
}
