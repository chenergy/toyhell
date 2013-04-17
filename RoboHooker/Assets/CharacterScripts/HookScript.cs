using UnityEngine;
using System.Collections;

public class HookScript : Weapon {
    private enum FireStage { Returning, Firing, Ready, Swinging };
    public float m_power;
    public float m_returnSpeed;
    public float m_minTimeBetweenFire;
    private float m_timer;
    private FireStage m_current;
    private GameObject m_hookedObject;
    private Hooker m_hooker;
    private Transform m_hookerTrans;
    private Transform m_real;

    public void Start()
    {
        m_current = FireStage.Ready;
        m_hooker = transform.parent.parent.gameObject.GetComponent<Hooker>();
        m_real = transform.parent;
        m_hookerTrans = transform.parent.parent;
    }

    public void Update()
    {
//        Debug.Log(m_current);
        m_timer = m_timer + Time.deltaTime;
        switch (m_current)
        {
            case FireStage.Returning:
                Vector3 returnVec = transform.localPosition.normalized*(-1);
                returnVec.z = 0;

                if (transform.localPosition.magnitude < m_returnSpeed*Time.deltaTime*2)
                {
                    transform.localPosition = Vector3.zero;
                    transform.localRotation = Quaternion.identity;
                    if (m_hookedObject != null)
                    {
                        m_hookedObject.transform.parent = null;
                    }
                    m_current = FireStage.Ready;
                }
                else
                {
                    transform.localPosition= transform.localPosition + (returnVec * Time.deltaTime* m_returnSpeed);
                }
                break;
            case FireStage.Swinging:
                Vector3 dir=m_hookerTrans.localPosition.normalized*(-1);
                m_hookerTrans.localPosition = m_hookerTrans.localPosition + (dir * Time.deltaTime * m_returnSpeed);
                break;
        }
    }

    public override void fire(Vector2 dir)
    {
        Debug.Log("Fire");
        if (m_timer > m_minTimeBetweenFire)
        {
            m_timer = 0;
            switch (m_current)
            {
                case FireStage.Ready:
                    Debug.Log("Firing dir " + dir.x * m_power + ", " + dir.y * m_power);
                    gameObject.AddComponent<BoxCollider>();
                    gameObject.AddComponent<Rigidbody>();
                    rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
                    m_current = FireStage.Firing;
                    rigidbody.AddForce(dir.x * m_power, dir.y * m_power, 0);
                    break;

                case FireStage.Firing:
                    realIn();
                    break;
                case FireStage.Swinging:
                    Debug.Log("drop");
                    m_hookerTrans.parent = null;
                    transform.parent = m_real;
                    m_hooker.Drop();
                    realIn();
                    break;
            }
        }

    }
    public void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if ((m_current==FireStage.Firing)&&(other.gameObject.tag=="Grabbable"))
        {
            Destroy(collider);
            Destroy(rigidbody);
            transform.parent = null;
            m_hooker.Swing = true;
            m_hookerTrans.parent = transform;
            m_current = FireStage.Swinging;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if ((m_current == FireStage.Firing) && (other.gameObject.tag != "Player")&&(other.transform.parent!=transform.parent.parent))
        {
            realIn();
            if (other.gameObject.GetComponent<SocketWeapon>() != null)
            {
                other.gameObject.transform.parent = gameObject.transform;
                m_hookedObject = other.gameObject;
            }
        }
    }
    private void realIn()
    {
        Debug.Log("realling in");
        m_current = FireStage.Returning;
        Destroy(collider);
        Destroy(rigidbody);
    }
}
