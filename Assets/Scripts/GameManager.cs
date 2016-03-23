/*
 * Attached to GameManager empty GameObject
 *
 * Features:
 * Signals fading class to fade the scene in or out
 * Singleton class
 * Controls scene navigation
 *
 * Written by Grace Barrett-Snyder
 *
 */

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
            DontDestroyOnLoad(gameManager); // Persistent in each scene
        }
        else
        {
            Destroy(this); // Destroys copies of this class
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
            { // Space is pressed
                StartCoroutine(LoadSceneAfterDelay(currentLevel - 1));
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            { // Esc is pressed
                Application.Quit(); // Close application
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
    }

    void OnLevelWasLoaded(int level)
    { // Fade in when the new scene is loaded
        currentLevel = level;
        fading.beginFade(-1);
    }

    public bool DoorOpened
    {
        set { doorOpened = value; }
    }
}
