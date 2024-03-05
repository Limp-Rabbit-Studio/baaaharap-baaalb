using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraController : MonoBehaviour
{
    #region - Declarations
    [SerializeField]
    private float duration;
    #endregion

    #region - Methods
    public void LookAt(Transform target)
    {
        transform.DOLookAt(target.position, duration);
    }
    #endregion
}
