using UnityEngine;
using System.Collections;

public class BarbieHead : Weapon {
    public float m_speed;
    public float m_life;
    private float m_timer;

    void Start()
    {
//        transform.position = new Vector3(transform.position.x, transform.position.y, -7f);
        rigidbody.AddForce( transform.forward * m_speed/.01f);
        m_timer = m_life;
    }
    public void Update()
    {
        if (m_timer > 0)
        {
            m_timer -= Time.deltaTime;
            if (m_timer < 0)
            {
                float z = transform.position.z;
                Ray centerScreen = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                Vector3 newPos = centerScreen.GetPoint(transform.position.z - Camera.main.transform.position.z);
                rigidbody.velocity = new Vector3();
                collider.isTrigger = false;
                rigidbody.useGravity = true;
                newPos.z = z;
                m_timer = m_life;
                transform.position = newPos;
                rigidbody.useGravity = true;
                //Debug.LogError("pause");
            }
        }
    }
    public override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        collider.isTrigger = true;
        Debug.Log(other.gameObject.name);
        EnemyInput enemy=other.gameObject.GetComponent<EnemyInput>();
        if (enemy != null)
        {
            Debug.Log("damaging");
            enemy.enemy.CurrentHP -= m_damage;
            Destroy(gameObject);
        }
    }
}
