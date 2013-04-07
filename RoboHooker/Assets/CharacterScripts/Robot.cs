using UnityEngine;
using System.Collections;

public class Robot : PlayerCharacter {

    public GameObject m_RightWeapon;
    public SocketedWeapon m_RightScript;
    public KeyCode m_RightEuipKey;

    protected override void pickUpWeapon(SocketedWeapon newWeapon)
    {
    }
}
