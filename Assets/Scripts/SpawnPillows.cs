using UnityEngine;
using System.Collections;

public class SpawnPillows : MonoBehaviour {

    public GameObject transformParentObject;
    public GameObject pillowPrefab;
	public GameObject tutorialPillowPrefab;

    public float angularDrag;
    public GameObject cursor;

    Transform[] transforms;

    void Awake()
    {
        // Get array of transforms where the pillows will spawn
        transforms = transformParentObject.GetComponentsInChildren<Transform>();
    }

	void Start () {
        spawnPillows();
	}

    // Instantiate Pillows
    void spawnPillows()
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            // Get current transform
			Transform currentTransform = transforms[i];
            GameObject currentPrefab;
            string name;

			// Tutorial pillow
			if (i == transforms.Length - 1) {
                currentPrefab = tutorialPillowPrefab;
                name = "Tutorial Pillow";
			}

            // Default pillows
			else {
                currentPrefab = pillowPrefab;
                name = "Pillow" + i; // Helps with debugging
			}

            // Instantiate a pillow with the current transform
            GameObject pillow = (GameObject)Instantiate(currentPrefab, currentTransform.position, currentTransform.rotation);

            // Name the pillow
            pillow.name = name;
           
            // Make pillow the child of this object
            pillow.transform.parent = gameObject.transform;

            // Add components to the pillow
            // Make the pillow interactable
            Interactable interactable = pillow.AddComponent<Interactable>();
            interactable.cursorObject = cursor;

            // Give the pillow a rigidbody
            Rigidbody rb = pillow.AddComponent<Rigidbody>();
            rb.angularDrag = angularDrag;
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            // Give the pillow a pillow core
            PillowCore pillowCore = pillow.gameObject.transform.GetChild(0).gameObject.AddComponent<PillowCore>();
            pillowCore.parentsRb = rb; // Set up reference to pillow's rigidbody
        }
    }
}
