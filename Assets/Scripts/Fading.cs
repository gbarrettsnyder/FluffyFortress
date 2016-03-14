using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fading : MonoBehaviour {

    public static Fading fading;
    GameObject fadeOutImage;
    Material material;

    int fadeDirection; 
    float start;
    float end;
    float inc;

    void Awake()
    {
        if (fading == null)
        {
            fading = this;
            fading.fadeDirection = -1; // direction to fade: in = -1 or out = 1
                //(GameObject)Instantiate(fadeOutImagePrefab, new Vector3(0, 0, Camera.main.transform.position.z + 1), Quaternion.identity);
            DontDestroyOnLoad(fading);
        }
        else
        {
            Destroy(this);
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
            fadeOutImage = GameObject.FindGameObjectWithTag("BlackScreen");
            material = fadeOutImage.GetComponent<Renderer>().material;
        }
        if (!fadeOutImage.activeSelf)
        {
            fadeOutImage.SetActive(true);
        }

        if (direction == -1)
        {
            start = 1f;
            end = 0f;
        }
        else if (direction == 1)
        {
            start = 0f;
            end = 1f;
        }

        inc = 0.1f * direction;

        StartCoroutine("Fade");
    }

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
            imgColor.a = a;
            material.color = imgColor;
            yield return new WaitForSeconds(.1f);
        }
        if (fadeDirection == -1)
        {
            fadeOutImage.SetActive(false);
        }
    }
}
