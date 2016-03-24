/*
 * Attached to each pillow (selectable object)
 * Uses WorldCrosshair cursor
 *
 * Features:
 * Changes object color when hovered over
 * Moves object with cursor when left mouse button is pressed
 * Rotates the object when right mouse button is pressed
 * Pushes/pulls the object with mouse wheel
 * Drops the object on mouse release
 *
 * Written by Grace and Richard Barrett-Snyder
 *
 * How to check if object exists at a certain position (using Physics.Raycast):
 * http://forum.unity3d.com/threads/check-if-object-exists-at-a-certain-position.161462/
 */

using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    public GameObject cursorObject; // Reference to Cursor GameObject in hierarchy
    WorldCrosshair cursor; // Reference to cursorObject's WorldCrosshair script

    Camera mainCam; // Reference to Main Camera
	
    Vector3 offset; // Reference to difference between Cursor and Main Camera in 3D space
	float offsetMagnitude; // Reference to length of offset

    bool selected; // Whether this object is selected by the cursor
	static GameObject selectedObject = null; // Reference to the current selected object (can only be one since it's static)

	bool hittingBoundary = false; // Whether the object is touching an object in front of it (ex: wall)
	
	Rigidbody rb; // Reference to this object's rigidbody
	Material myMaterial; // Reference to my material
	Color myColor; // Reference to this object's color

    void Awake()
    {
        mainCam = Camera.main;
		cursor = cursorObject.GetComponent<WorldCrosshair>();
    }

	void Start () {
		// The following initializations are in Start since the Pillow Prefabs are instantiated in an Awake function
        rb = GetComponent<Rigidbody>();
		myMaterial = GetComponent<Renderer>().material;
		myColor = myMaterial.color;
    }

	void Update () {

		bool hover;

		// Hover is true when the world cursor is on this object
		hover = (cursor.selectedObject == gameObject);

		// If the cursor is on me, highlight white
		myMaterial.color = hover ? Color.white : myColor;

		// If the mouse is pressed when the cursor is on thos object, and if there is nothing else selected, it is now selected
		// Or, if this object was selected previously, it is still selected
		if (!selected && hover && Input.GetMouseButton (0) && selectedObject == null) {  // Only want to fire this once
			selected = true;
            selectedObject = gameObject;

			offset = cursor.transform.position - mainCam.transform.position;
			offsetMagnitude = offset.magnitude;

            rb.isKinematic = true; // Object won't move on its own
        }

		if (selected) {
			// If this object is selected, color it white
			myMaterial.color = Color.white;

			// If the left mouse button clicked or pressed
            if (Input.GetMouseButton(0))
            {
                Vector3 t = cursor.transform.position - mainCam.transform.position;
                t = t.normalized * offsetMagnitude;

                Vector3 newPosition = mainCam.transform.position + t;
				
				// If there isn't another object at this position
                if (Physics.Raycast(rb.position, newPosition, 2) != true)
                {
					// Move this object to the new position
                    rb.position = newPosition;
                    hittingBoundary = false;
                }
                else
                { // If there is an object at this position
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
				// Assumes the user will only encounter a boundary moving the object forward
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

			// If right mouse button clicked or pressed
			if (Input.GetMouseButton(1))
			{
				// Rotate the object on y-axis
				// 2 can be replaced with any number
                transform.Rotate(0, rb.rotation.y + 2, 0);
            }
		}
    }
}
