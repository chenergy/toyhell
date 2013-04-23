using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	
	public GameObject Hooker;
	public GameObject Robot;
	
	public float minDistance = -10;
	public float maxDistance = -2000;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Vector3 difference = (Hooker.transform.position - Robot.transform.position);
		Vector3 posDifference = Robot.transform.position + difference/2;
		float zPosition = -difference.magnitude;
		this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(posDifference.x, posDifference.y, Mathf.Clamp(zPosition, maxDistance, minDistance)), Time.deltaTime*10);
		
	}
}
