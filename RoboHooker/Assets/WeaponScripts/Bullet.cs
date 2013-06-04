using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public Vector3 m_dir;
    public float m_speed;
    public float m_range;
    public int m_damage;

    public void Start(){
        transform.position = new Vector3(transform.position.x, transform.position.y, -7f);
    }

    public void Update()
    {
        rigidbody.MovePosition(transform.position + (m_speed * m_dir * Time.deltaTime));
        if (!renderer.isVisible || m_range < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            m_range -= Time.deltaTime * m_speed;
        }
    }
    public void OnCollisionEnter(Collision other)
    {
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
