/*
 * Attached to pillows
 * Uses WorldCrosshair cursor
 *
 * Features:
 * Changes object color when hovered over
 * Moves object with cursor when left mouse button is pressed
 * Rotates the object when right mouse button is pressed
 * Pushes/pulls the object with mouse wheel
 *
 * Written with Richard Barrett-Snyder
 */


using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    public GameObject cursorObject;
    WorldCrosshair cursor;

    Camera mainCam;
    Rigidbody rb;
	
    Vector3 offset;
	float offsetMagnitude;

    bool selected;

	Material myMaterial;
	Color myColor;

	static GameObject selectedObject = null;
    bool hittingBoundary = false;

    void Awake()
    {
        mainCam = Camera.main;
    }

	void Start () {
        rb = GetComponent<Rigidbody>();
		myMaterial = GetComponent<Renderer>().material;
		myColor = myMaterial.color;
        cursor = cursorObject.GetComponent<WorldCrosshair>();
    }

	void Update () {

		bool hover;

		// hover is true when the world cursor is on me
		hover = (cursor.selectedObject == gameObject);

		// If the cursor is on me, highlight white
		myMaterial.color = hover ? Color.white : myColor;

		// If the mouse is pressed when the cursor is on me, I'm now selected
		// Or, if I was selected previously, I am still selected
		if (!selected && hover && Input.GetMouseButton (0) && selectedObject == null) {  // Only want to fire this once
			selected = true;
            selectedObject = gameObject;

			offset = cursor.transform.position - mainCam.transform.position;
			offsetMagnitude = offset.magnitude;

            rb.isKinematic = true;
        }

		if (selected) {
			myMaterial.color = Color.white;

            if (Input.GetMouseButton(0))
            {
                Vector3 t = cursor.transform.position - mainCam.transform.position;
                t = t.normalized * offsetMagnitude;

                Vector3 newPosition = mainCam.transform.position + t;
                if (Physics.Raycast(rb.position, newPosition, 2) != true)
                {
                    rb.position = newPosition;
                    hittingBoundary = false;
                }
                else
                {
                    hittingBoundary = true;
                }
            }
            else
            {
                // Deselect
                selected = false;
                selectedObject = null;
                rb.isKinematic = false; // Pillow will drop normally
            }

			// If the scroll wheel is used
			if (Input.GetAxis("Mouse ScrollWheel") != 0)
			{
                // Push pillow away
				if (Input.GetAxis("Mouse ScrollWheel") > 0 && ! hittingBoundary)
				{
					offsetMagnitude *= 1.05f;
				}
				
                // Pull pillow closer
				else if (Input.GetAxis("Mouse ScrollWheel") < 0)
				{
					offsetMagnitude /= 1.05f;
				}
			}

			// If right click
			if (Input.GetMouseButton(1))
			{
				// Zoom Value = 2
                transform.Rotate(0, rb.rotation.y + 2, 0);
            }
		}
    }
}
