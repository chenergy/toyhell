using UnityEngine;
using System.Collections;

public class Robot : PlayerCharacter {

    public GameObject m_RightWeapon;
    public SocketWeapon m_RightScript;
    public KeyCode m_RightEuipKey;

    protected override void pickUpWeapon(SocketWeapon newWeapon)
    {
    }
}
