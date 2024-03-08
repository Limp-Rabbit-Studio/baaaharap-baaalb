using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    SkinnedMeshRenderer rend;
    int upgrades = 0;

    private void Awake()
    {
        rend = GetComponentInChildren<SkinnedMeshRenderer>();
        rend.material.color = Color.white;
        upgrades = 0;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Upgrade()
    {
        upgrades++;
        float value = 1 - .2f * upgrades;
        if (value < 0) value = 0;
        rend.material.color = new Color(value, value, value);
    }
}
