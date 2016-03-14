using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public GameObject doorSign;
    public GameObject cursorObject;
    WorldCrosshair cursor;

    Material myMaterial;
    Color myColor;

    // Use this for initialization
    void Start()
    {
        cursor = cursorObject.GetComponent<WorldCrosshair>();

        myMaterial = GetComponent<Renderer>().material;
        myColor = myMaterial.color;
    }

    void Update()
    {
        bool hover;

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
