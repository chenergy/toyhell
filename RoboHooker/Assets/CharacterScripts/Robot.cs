using UnityEngine;
using System.Collections;

public class Robot : PlayerCharacter
{

    public GameObject m_RightWeapon;
    public SocketWeapon m_RightScript;
    public KeyCode m_RightEuipKey;
    public Transform m_CannonAim;
    private bool m_Loaded = true;

    public override void Update()
    {
        base.Update();
        Vector2 aim = new Vector2(Input.GetAxis(m_controller.m_AimAxisX), Input.GetAxis(m_controller.m_AimAxisY)).normalized;
        if (aim.x != m_CannonAim.transform.forward.x || aim.y != m_CannonAim.transform.forward.y)
        {
            if (transform.forward.x > 0)
            {
                aim.x = aim.x * (-1);
            }
            float rot = Vector2.Angle(new Vector2(m_CannonAim.transform.forward.x, m_CannonAim.transform.forward.z), aim);
            m_CannonAim.transform.Rotate(new Vector3(0, 0, 1), rot);
        }
    }
}
