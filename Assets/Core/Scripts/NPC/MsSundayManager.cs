using System.Collections;
using UnityEngine;

public class MsSundayManager : Singleton<MsSundayManager>
{
    [SerializeField] GameObject dialogObject;
    [SerializeField] GameObject fadeScreen;
    [SerializeField] GameObject illustration1;
    [SerializeField] GameObject illustration2;

    public override void Awake()
    {
        base.Awake();
        dialogObject.SetActive(false);
        fadeScreen.SetActive(false);
        illustration1.SetActive(false);
        illustration2.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayDialog()
    {
        dialogObject.SetActive(true);
    }

    public void HideDialog()
    {
        dialogObject.SetActive(false);
    }

    public void ActivateHug(GameObject msSunday)
    {
        StartCoroutine(HugCouroutine());
        Destroy(msSunday);
    }

    IEnumerator HugCouroutine()
    {
        HideDialog();
        illustration1.SetActive(true);
        yield return new WaitForSeconds(1f);
        illustration1.SetActive(false);
        illustration2.SetActive(true);
        yield return new WaitForSeconds(1f);
        illustration2.SetActive(false);
    }
}
