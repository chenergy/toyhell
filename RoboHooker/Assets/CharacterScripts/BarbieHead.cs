using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BarbieHead : Weapon {
    public float m_speed;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -7f);
        rigidbody.AddForce( transform.forward * m_speed/.01f);
    }
}
