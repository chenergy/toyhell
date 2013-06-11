using UnityEngine;
using System.Collections;

public class SocketWeapon : Weapon {

    public AnimationClip m_idleAnimation;
    public Transform m_socketJoint;
    public Animation m_animationController;
    public float m_fireRate;
    protected float m_timer;

    public void Start()
    {
    }

    public void Equip(GameObject Character)
    {
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
    public virtual void Update()
    {
        if (m_timer < m_fireRate)
        {
            m_timer += Time.deltaTime;
        }
    }
    protected void PlayClip(AnimationClip ac, WrapMode mode)
    {
        if ((ac != null) && (animation != null))
        {
            if (!animation.IsPlaying(ac.name))
            {
                animation.wrapMode = mode;
                animation.Play(ac.name);
            }
        }
    }
}