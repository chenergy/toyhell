using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {

    public GameObject m_LeftWeapon;
    public GameObject m_RightWeapon;

    public SocketedWeapon m_LeftScript;
    public SocketedWeapon m_RightScript;

    public KeyCode m_LeftKey;
    public KeyCode m_RightKey;
    public KeyCode m_JumpKey;
    public KeyCode m_LeftEquipKey;
    public KeyCode m_RightEuipKey;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
