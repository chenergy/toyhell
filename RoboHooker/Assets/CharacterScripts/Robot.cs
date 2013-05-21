using UnityEngine;
using System.Collections;

public class Robot : PlayerCharacter
{

    public GameObject m_RightWeapon;
    public SocketWeapon m_RightScript;
    public KeyCode m_RightEuipKey;
    private bool m_Loaded = true;

    public override void Update()
    {
        base.Update();
    }
}
