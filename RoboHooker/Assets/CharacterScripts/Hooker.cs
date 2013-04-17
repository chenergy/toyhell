using UnityEngine;
using System.Collections;

public class Hooker : PlayerCharacter {
    private bool m_swinging=false;
    public bool Swing
    {
        set
        {
            m_swinging = value;
        }
    }
    public override void Update()
    {
        float m_fire = Input.GetAxis(m_Attack);

        if (!m_swinging)
        {
            base.Update();
        }
        else if (m_fire > 0)
        {
            m_mainWeaponScript.fire(Vector2.zero);
        }

    }
    public void Drop()
    {
        m_swinging = false;
    }
}
