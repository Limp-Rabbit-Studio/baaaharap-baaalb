using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlideShow : MonoBehaviour
{

    [SerializeField] private GameObject[] slides;
    // [SerializeField] private Image image;
    // [SerializeField] private Sprite[] allSprites;
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
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(false);
        }

        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource component is added to the GameObject
        if (playContinuousSound && continuousSound != null)
        {
            audioSource.clip = continuousSound;
            audioSource.loop = true; // Loop the continuous sound
            audioSource.Play();
        }
        // StartCoroutine(PlaySlideShow());
        slideIndex = 0;
        slides[slideIndex].SetActive(true);
    }

    IEnumerator PlaySlideShow()
    {
        yield return new WaitForSeconds(startGap);

        for (currentSpriteIndex = 0; currentSpriteIndex < slides.Length; currentSpriteIndex++)
        {
            slides[currentSpriteIndex].SetActive(true);

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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    int slideIndex = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // skipRequested = true;
            slideIndex++;
            if (slideIndex >= slides.Length)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                slides[slideIndex - 1].SetActive(false);
                slides[slideIndex].SetActive(true);
            }
        }
    }
}
