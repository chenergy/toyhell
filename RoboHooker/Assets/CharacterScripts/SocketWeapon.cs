using UnityEngine;
using System.Collections;

public class SocketWeapon : Weapon {
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
