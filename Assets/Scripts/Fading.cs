/*
 * Attached to GameManager
 *
 * Features:
 * Controls fading in and out between scenes
 * Singleton class
 *
 * Written by Grace Barrett-Snyder
 *
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fading : MonoBehaviour {

    public static Fading fading; // Static instance
	
    GameObject fadeOutImage; // BlackScreen object used for fading in and out
    Material material; // Material of fadeOutImage

    int fadeDirection; // Whether we are fading in or out
    float start; // Starting opacity
    float end; // Ending opacity
    float inc; // Increment for opacity change

    void Awake()
    {
        if (fading == null)
        {
            fading = this;
            fading.fadeDirection = -1; // Direction to fade: in = -1 or out = 1
            DontDestroyOnLoad(fading); // Can be used in multiple scenes
        }
        else
        {
            Destroy(this); // Destroy any copies of this class
        }
    }

    void Start()
    {
        // Fade in start screen
        beginFade(-1);
    }

    // Sets fadeDirection making the scene fade in if -1 and out if 1
    public void beginFade (int direction)
    {
        fadeDirection = direction;

        if (fadeOutImage == null)
        {
			// This object changes in each scene, so we need to find it again
            fadeOutImage = GameObject.FindGameObjectWithTag("BlackScreen");
            material = fadeOutImage.GetComponent<Renderer>().material;
        }
        if (!fadeOutImage.activeSelf)
        {
            fadeOutImage.SetActive(true);
        }

        if (direction == -1)
        { // Fade in
            start = 1f;
            end = 0f;
        }
        else if (direction == 1)
        { // Fade out
            start = 0f;
            end = 1f;
        }

        inc = 0.1f * direction;

        StartCoroutine("Fade");
    }

	// Flips the loop in Fade function depending on whether we are fading in or out
    bool loopCondition(float x)
    {
        if (fadeDirection == 1)
        {
            return x < end + inc;
        }
        else
        {
            return x > end + inc;
        }
    }

    IEnumerator Fade()
    {
        for (float a = start; loopCondition(a); a += inc)
        {
            Color imgColor = material.color;
            imgColor.a = a; // Decreased opacity
            material.color = imgColor;
            yield return new WaitForSeconds(.1f);
        }
        if (fadeDirection == -1)
        {
            fadeOutImage.SetActive(false); // Deactivate
        }
    }
}
