using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideShow : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] allSprites;
    [SerializeField] private AudioClip[] slideSounds; // Array of sound clips for each slide
    [SerializeField] private AudioClip transitionSound; // Sound clip for transitioning between slides
    [SerializeField] private AudioClip continuousSound; // Continuous sound clip to play over all slides
    [SerializeField] private float startGap, gapBetweenEachImage;
    [SerializeField] private bool playContinuousSound; // Checkbox to select continuous sound play

    private AudioSource audioSource;
    private bool skipRequested = false;
    private int currentSpriteIndex = 0;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource component is added to the GameObject
        if (playContinuousSound && continuousSound != null)
        {
            audioSource.clip = continuousSound;
            audioSource.loop = true; // Loop the continuous sound
            audioSource.Play();
        }
        StartCoroutine(PlaySlideShow());
    }

    IEnumerator PlaySlideShow()
    {
        yield return new WaitForSeconds(startGap);

        for (currentSpriteIndex = 0; currentSpriteIndex < allSprites.Length; currentSpriteIndex++)
        {
            image.sprite = allSprites[currentSpriteIndex];

            // Play the sound for the current slide if not playing continuous sound
            if (!playContinuousSound && slideSounds != null && currentSpriteIndex < slideSounds.Length && slideSounds[currentSpriteIndex] != null)
            {
                audioSource.clip = slideSounds[currentSpriteIndex];
                audioSource.Play();
            }

            yield return new WaitUntil(() => skipRequested || Time.time >= startGap + (currentSpriteIndex + 1) * gapBetweenEachImage);
            if (skipRequested)
            {
                skipRequested = false;
                // Play the transition sound if space was pressed
                if (transitionSound != null)
                {
                    audioSource.PlayOneShot(transitionSound);
                }
                continue;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skipRequested = true;
        }
    }
}
