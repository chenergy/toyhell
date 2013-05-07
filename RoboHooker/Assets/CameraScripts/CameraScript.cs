using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	
	private GameObject hooker;
	private GameObject robot;
	
	public float minDistance = 15;
	public float maxDistance = 2000;
	public float YOffset = 3;
	public float ZOffset = -3;
	
	// Use this for initialization
	void Start () {
		hooker = GameObject.Find("Hooker");
		robot = GameObject.Find("Robot");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 difference = (hooker.transform.position - robot.transform.position);
		Vector3 posDifference = robot.transform.position + difference/2;
		float zPosition = -difference.magnitude;
		this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(posDifference.x, posDifference.y + YOffset, Mathf.Clamp(zPosition, -maxDistance, -minDistance) + ZOffset), Time.deltaTime*10);
	}
}
