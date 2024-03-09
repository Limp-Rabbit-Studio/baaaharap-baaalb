using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HarelessBoss : MonoBehaviour
{
    SpriteRenderer sRend;
    AudioSource audioSrc;
    int health;
    bool isFlashing = false;

    // Start is called before the first frame update
    void Start()
    {
        sRend = GetComponent<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource>();
        health = 3;
        isFlashing = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hit()
    {
        if (isFlashing)
        {
            return;
        }
        audioSrc.Play();
        health--;
        if (health <= 0)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            StartCoroutine(FlashCor());
        }
    }

    IEnumerator FlashCor()
    {
        isFlashing = true;
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < 20; i++)
        {
            float cDelta = Random.Range(0, .3f);
            sRend.color = new Color(cDelta + .4f, cDelta + .1f, cDelta);
            yield return new WaitForSeconds(Random.Range(.05f, .1f));
            sRend.color = Color.white;
            yield return new WaitForSeconds(Random.Range(.05f, .1f));
        }
        isFlashing = false;
    }
}
