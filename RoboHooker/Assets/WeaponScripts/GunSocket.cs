using UnityEngine;
using System.Collections;

public class GunSocket : SocketWeapon {
    public GameObject m_bullet;
    public float m_bulletSpeed;
    public float m_range;
    public float m_fireRate;
    private float m_timer;
    public void Update()
    {
        if (m_timer < m_fireRate)
        {
            m_timer += Time.deltaTime;
        }
    }
    public override void fire(Vector2 dir)
    {
        if (m_timer > m_fireRate)
        {
            Bullet ps = (Bullet)((GameObject)Instantiate(m_bullet, transform.position, transform.rotation)).GetComponent<Bullet>();
            Debug.Log(dir + " is dir");
            ps.m_dir = new Vector3(dir.x, 0, 0);
            ps.m_damage = m_damage;
            ps.m_speed = m_bulletSpeed;
            ps.m_range = m_range;
            m_timer = 0;
        }
    }
}
