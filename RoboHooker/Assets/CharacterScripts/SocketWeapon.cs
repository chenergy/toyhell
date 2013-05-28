using UnityEngine;
using System.Collections;

public class SocketWeapon : Weapon {

    public AnimationClip m_idleAnimation;

    public void Equip(GameObject Character)
    {
        transform.parent=Character.transform;
    }
    public void Deequip()
    {
        transform.parent = null;
    }
    public override void fire(Vector2 dir)
    {
        base.fire(dir);
    }
    public void Update()
    {
        if (!animation.IsPlaying(m_fireAnimation.name))
        {
            if (!animation.IsPlaying(m_idleAnimation.name))
            {
                animation.wrapMode = WrapMode.Loop;
                animation.Play(m_idleAnimation.name);
            }
        }
    }

}
