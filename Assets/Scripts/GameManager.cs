using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    Fading fading;
    int currentLevel;
    bool doorOpened;

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            gameManager.fading = GetComponent<Fading>();
            currentLevel = 0;
            gameManager.doorOpened = false;
            DontDestroyOnLoad(gameManager);
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        if ((currentLevel == 0 && Input.GetKeyDown(KeyCode.Space)) || (currentLevel == 1 && doorOpened))
        {
            doorOpened = false;
            StartCoroutine(LoadSceneAfterDelay(currentLevel + 1));
        }
        else if (currentLevel == 2)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(LoadSceneAfterDelay(currentLevel - 1));
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    public IEnumerator LoadSceneAfterDelay(int level)
    {
        // Fade out current scene
        fading.beginFade(1);

        // Wait while scene is fading out
        yield return new WaitForSeconds(2);

        // Load next scene
        Application.LoadLevel(level);

        //yield return new WaitForSeconds(0.02f);
        // Fade in new scene
        //fading.beginFade(-1);
    }

    void OnLevelWasLoaded(int level)
    {
        currentLevel = level;
        fading.beginFade(-1);
    }

    public bool DoorOpened
    {
        set { doorOpened = value; }
    }
}
