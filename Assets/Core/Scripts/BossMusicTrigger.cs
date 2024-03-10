
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private BGMusic bgMusic;

    public void Init(BGMusic musicScript)
    {
        bgMusic = musicScript;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && bgMusic != null)
        {
            bgMusic.PlayBossMusic();
        }
    }
}