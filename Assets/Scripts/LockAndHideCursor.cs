﻿/*
 * Locks and Hides Default Cursor
 * 
 * Written by Grace Barrett-Snyder
 */

using UnityEngine;
using System.Collections;

public class LockAndHideCursor : MonoBehaviour {
	
	void Start () {
		// Lock cursor
		Cursor.lockState = CursorLockMode.Locked;

		// Hide cursor
		Cursor.visible = false;
	}
}
