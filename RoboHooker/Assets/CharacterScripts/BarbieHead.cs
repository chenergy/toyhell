using UnityEngine;
using System.Collections;

public class BarbieHead : Weapon {
    public float m_speed;
    public float m_life;
    private float m_timer;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -7f);
        rigidbody.AddForce( transform.forward * m_speed/.01f);
        m_timer = m_life;
    }
    public void Update()
    {
        m_timer -= Time.deltaTime;
        if (m_timer < 0)
        {
            float z = transform.position.z;
            Ray centerScreen = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Vector3 newPos = centerScreen.GetPoint(transform.position.z - Camera.main.transform.position.z);
            Destroy(rigidbody);
            Debug.LogError("pause");
            rigidbody.useGravity = true;
            newPos.z = z;
            m_timer = m_life;
            transform.position=newPos;
            rigidbody.useGravity = true;
        }
        if (rigidbody == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
    }
    public override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        collider.isTrigger = true;
    }
}
