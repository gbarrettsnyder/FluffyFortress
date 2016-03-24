using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class RecenterOculus : MonoBehaviour {

	void Start () {
        InputTracking.Recenter();
	}
}
