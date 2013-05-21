using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	
	private GameObject hooker;
	private GameObject robot;
	
	public float minDistance = 15;
	public float maxDistance = 2000;
	public float YOffset = 7;
	public float ZOffset = -15;
	
	// Use this for initialization
	void Start () {
		hooker = GameObject.Find("Hooker");
		robot = GameObject.Find("Robot");
	}
	
	// Update is called once per frame
	void LateUpdate () {
        
        Vector3 posDifference ;
        float zPosition;
        if ((hooker != null) && (robot != null))                //Higgs-Bugson avoided http://www.codinghorror.com/blog/2012/07/new-programming-jargon.html see 11
        {
            Vector3 difference = (hooker.transform.position - robot.transform.position);
            posDifference = robot.transform.position + difference / 2;
            zPosition = -difference.magnitude;
        }
        else
        {
            hooker = GameObject.Find("Hooker");             //maybe just needs to be reassigned because respawned
            robot = GameObject.Find("Robot");
            zPosition = minDistance;
            if (hooker != null)
            {
                posDifference = hooker.transform.position;
            }
            else
            {
                posDifference = robot.transform.position;
            }
        }
		this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(posDifference.x, posDifference.y + YOffset, Mathf.Clamp(zPosition, -maxDistance, -minDistance) + ZOffset), Time.deltaTime*10);
	}
}
