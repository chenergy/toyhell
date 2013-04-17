using UnityEngine;
using System.Collections;

public class PencilScript : SocketWeapon {

    Animator m_animator;

    public void Start()
    {
        m_animator = GetComponent<Animator>();
    }
    public override void fire(Vector2 dir)
    {
        m_animator.StartPlayback();
    }
}
