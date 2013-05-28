using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    public int m_damage;
    public AnimationClip m_fireAnimation;

	// Update is called once per frame
    public virtual void fire(Vector2 dir)
    {
        if (m_fireAnimation != null)
        {
            if (!animation.IsPlaying(m_fireAnimation.name))
            {
                animation.wrapMode = WrapMode.Once;
                animation.Play(m_fireAnimation.name);
            }
        }
    }
    public virtual void OnCollisionEnter(Collision other)
    {
        foreach (ContactPoint cp in other.contacts)
        {
            Debug.Log(cp.otherCollider.gameObject.name + " is hit");
            EnemyInput ei = (EnemyInput)cp.otherCollider.gameObject.GetComponent<EnemyInput>();
            if (ei != null)
            {
                Debug.Log("dealing " + m_damage);
                ei.currentHP -= m_damage;
            }
        }
    }
}
