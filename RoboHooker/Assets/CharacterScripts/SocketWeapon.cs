using UnityEngine;
using System.Collections;

public class SocketWeapon : Weapon {

    public AnimationClip m_idleAnimation;
    public Transform m_socketJoint;

    public void Start()
    {
    }

    public void Equip(GameObject Character)
    {
        Debug.Log("Equipping");
        collider.isTrigger = false;
    }
    public void Deequip()
    {
        m_socketJoint.parent = null;
        collider.isTrigger = true;
    }
    public override void fire(Vector2 dir)
    {
        base.fire(dir);
    }
    public void Update()
    {

    }
}
