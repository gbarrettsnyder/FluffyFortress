using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class RecenterOculus : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InputTracking.Recenter();
	}
}
