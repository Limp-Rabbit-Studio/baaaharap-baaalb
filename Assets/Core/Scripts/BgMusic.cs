// BGMusic.cs
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMusic : MonoBehaviour
{
    public AudioClip mainSoundtrack;
    public AudioClip bossMusic;
    [Range(0f, 1f)] public float volume = 0.5f;
    public Collider bossAreaCollider;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.clip = mainSoundtrack;
        audioSource.Play();

        if (bossAreaCollider != null)
        {
            BossTrigger bossTrigger = bossAreaCollider.gameObject.GetComponent<BossTrigger>();
            if (bossTrigger == null)
            {
                bossTrigger = bossAreaCollider.gameObject.AddComponent<BossTrigger>();
            }
            bossTrigger.Init(this);
        }
    }

    public void PlayBossMusic()
    {
        if (audioSource.clip != bossMusic)
        {
            audioSource.clip = bossMusic;
            audioSource.Play();
        }
    }
}
