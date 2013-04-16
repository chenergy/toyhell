using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    protected KeyCode m_fireKey;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(m_fireKey))
            fire();
	}
    protected virtual void fire()
    {
    }
}
