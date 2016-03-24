/*
 * Attached to Door GameObject
 * Uses WorldCrosshair cursor
 *
 * Features:
 * Detects whether the cursor is over the Door
 * Changes Door color when hovered over and reveals the door sign
 * Alerts the GameManager when the door sign is pressed
 *
 * Written by Grace Barrett-Snyder
 *
 */
 
using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public GameObject doorSign; // Reference to sign on Door
    public GameObject cursorObject; // Reference to Cursor
    WorldCrosshair cursor;

    Material myMaterial; // Reference to Door's material
    Color myColor; // Reference to Door's color
	
	bool hover; // Whether the cursor is hovering over the door

    void Start()
    {
        cursor = cursorObject.GetComponent<WorldCrosshair>();

        myMaterial = GetComponent<Renderer>().material;
        myColor = myMaterial.color;
    }

    void Update()
    {
        hover = (cursor.selectedObject == gameObject);

        // If the cursor is on me, highlight white
        if (hover)
        {
            myMaterial.color = Color.white;
            doorSign.SetActive(true);
        }
        else if (doorSign.activeSelf == true)
        {
            doorSign.SetActive(false);
        }
        
        // If the mouse is pressed when the cursor is on me, I'm now selected
        // Or, if I was selected previously, I am still selected
        if (hover && Input.GetMouseButtonDown(0))
        {  // Only want to fire this once
            GameManager.gameManager.DoorOpened = true;
        }
    }
}
