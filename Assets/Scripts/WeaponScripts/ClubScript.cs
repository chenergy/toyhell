using UnityEngine;
using System.Collections;
/*
public class ClubScript : SocketWeapon
{
    private bool m_attacking;

    public override void Update()
    {
        if (!collider.isTrigger)
        {
            base.Update();
            if (!m_animationController.IsPlaying(m_fireAnimation.name))
            {
                m_attacking = false;
            }
        }
    }

    public override void fire(Vector2 dir)
    {
        if (m_timer > m_fireRate)
        {
            PlayClip(m_fireAnimation, WrapMode.Once);
            m_attacking = true;
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        if (m_attacking)
        {
            EnemyInput enemy = other.gameObject.GetComponent<EnemyInput>();
            if (enemy != null)
            {
                Debug.Log("damaging");
                enemy.DamageEnemy(m_damage);
                Destroy(gameObject);
            }
        }
    }
}
 */