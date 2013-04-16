using UnityEngine;
using System.Collections;

public class SocketWeapon : Weapon {

    public void Equip(GameObject Character)
    {
        transform.parent=Character.transform;
    }
    public void Deequip()
    {
        transform.parent = null;
    }

    public KeyCode fireKey
    {
        get
        {
            return m_fireKey;
        }
        set
        {
            m_fireKey = value;
        }
    }
}
