using UnityEngine;
using System.Collections;

public class Cannon : Weapon
{
    public GameObject m_barbieHead;
    public GameObject m_CannonLoc;
    private bool m_loaded=true;
    private GameObject m_firedHead;
    public override void fire(Vector2 dir)
    {
        if (m_loaded)
        {
            if (dir.magnitude < .2)
            {
                dir.x = transform.forward.x;
            }
            m_loaded = false;
            Debug.Log("dir is " + dir);
            m_firedHead=(GameObject) Instantiate(m_barbieHead, 
                m_CannonLoc.transform.position+transform.forward*2, transform.rotation);
            m_firedHead.GetComponent<Weapon>().m_damage = m_damage;
        }
    }
    public override void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint cp in collision.contacts)
        {
            Debug.Log("collision" + cp.otherCollider.name);
        }
        if (collision.gameObject == m_firedHead)
        {
            Destroy(collision.gameObject);
            m_loaded = true;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        OnCollisionEnter(collision);
    }
    /*
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject == m_firedHead)
        {
            Destroy(collision.gameObject);
            m_loaded = true;
        }
    }
     */

}
