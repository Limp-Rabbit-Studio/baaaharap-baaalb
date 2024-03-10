using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    [SerializeField] Texture[] textures;

    SkinnedMeshRenderer rend;
    int upgrades = 0;

    private void Awake()
    {
        rend = GetComponentInChildren<SkinnedMeshRenderer>();
        rend.material.color = Color.white;
        upgrades = 0;
        rend.material.SetTexture("_MainTex", textures[upgrades]);
        transform.localScale = Vector3.one;
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
        if (upgrades >= textures.Length)
        {
            return;
        }
        GetComponent<PlayerStats>().HealPlayer(1000);
        GetComponent<SheepDashAttack>().Upgrade();
        rend.material.mainTexture = textures[upgrades];
        // rend.material.SetTexture("Albedo", textures[upgrades]);
        transform.localScale = Vector3.one * (1 + upgrades * .04f);

        // float value = 1 - .2f * upgrades;
        // if (value < 0) value = 0;
        // rend.material.color = new Color(value, value, value);
    }
}
