using UnityEngine;
using System.Collections;

public class NonRenderedText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.renderer.enabled = false;
	}
}
