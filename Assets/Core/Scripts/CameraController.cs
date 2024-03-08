using UnityEngine;
using DG.Tweening;
public class CameraController : MonoBehaviour
{
    #region - Declarations
    [SerializeField] private float location1;
    [SerializeField] private float location2;
    [SerializeField] private float duration;
    [SerializeField] private float speed = 3f;

    Transform crtDest;
    #endregion

    #region - Methods
    public void LookAt(Transform target)
    {
        transform.DOLookAt(target.position, duration);
    }

    public void UpdateCrtDest(Transform t)
    {
        crtDest = t;
    }

    public void Update()
    {
        if (crtDest != null)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                crtDest.position,
                Time.deltaTime * speed);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                crtDest.rotation,
                Time.deltaTime * speed);
        }
    }
    #endregion
}
