using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BarbieHead : MonoBehaviour {
    public float m_speed;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -7f);
        rigidbody.velocity = transform.forward * m_speed;
        Debug.Log("for"+transform.forward);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
/*        if (other.gameObject == m_firedHead)
        {
            Destroy(other.gameObject);
            m_loaded = true;
        }
*/    }
}
