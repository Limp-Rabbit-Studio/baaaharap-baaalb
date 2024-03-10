using UnityEditor.Rendering;
using UnityEngine;

public class PlayerLockDetector : MonoBehaviour
{
    public GameObject dialog;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasKey && inLockRange && Input.GetKeyDown(KeyCode.E) && lockObject)
        {
            hasKey = false;
            inLockRange = false;
            Destroy(lockObject);
            lockObject = null;
            dialog.gameObject.SetActive(false);
        }
    }

    bool hasKey = false;
    bool inLockRange = false;
    GameObject lockObject = null;

    private void OnTriggerEnter(Collider other)
    {
        LockTag lockTag = other.gameObject.GetComponent<LockTag>();
        KeyTag keyTag = other.gameObject.GetComponent<KeyTag>();
        if (keyTag != null)
        {
            hasKey = true;
            Destroy(keyTag.gameObject);
        }
        if (lockTag != null)
        {
            lockObject = lockTag.gameObject;
            inLockRange = true;
            if (hasKey)
            {
                dialog.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        LockTag lockTag = other.gameObject.GetComponent<LockTag>();

        if (lockTag != null)
        {
            inLockRange = false;
            lockObject = null;
            dialog.gameObject.SetActive(false);
        }
    }
}
