using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : MonoBehaviour {

    public float m_movementSpeed;
    public float m_jumpSpeed;

    //Don't have a controller at the moment, will get one tomorrow
    public KeyCode m_LeftKey;
    public KeyCode m_RightKey;
    public KeyCode m_JumpKey;

    //Will hard code these, temporary leaving them public to allow for quick changes
    //should be possible to setup keyboard equivalents in the input manager
    //they should be set in stone by April 23, 2013
    public string m_LeftEquipKey;
    public string m_Attack;
    public string m_jumpButton;
    public string m_MoveAxis;
    public string m_AimAxisX;
    public string m_AimAxisY;

    public Weapon m_mainWeaponScript;
    public GameObject m_socketLoc;
    public GameObject m_mainWeapon;
    public GameObject m_LeftWeapon;
    public SocketWeapon m_LeftScript;

    private Vector3 m_movement;
    private CharacterController m_control;

    // Use this for initialization
    void Start()
    {
        m_control = (CharacterController)GetComponent<CharacterController>();
        m_movement = new Vector3();
    }
    // Update is called once per frame
    public virtual void Update()
    {
        float m_Movedir = Input.GetAxis(m_MoveAxis);
        float m_fire = Input.GetAxis(m_Attack);
        
        Vector2 m_Aim = new Vector2(Input.GetAxis(m_AimAxisX), Input.GetAxis(m_AimAxisY));
        if (m_fire != 0)
        {
            if (m_fire > 0)
                m_mainWeaponScript.fire(m_Aim);
            else if (m_LeftWeapon != null)
            {
                m_LeftScript.fire(m_Aim);
            }
        }
        m_movement.x = m_movementSpeed * m_Movedir;
        applyGravity();
        if ((Input.GetKey(m_JumpKey)||Input.GetKey(m_jumpButton)) && m_control.isGrounded)
        {
            m_movement.y = m_jumpSpeed;
        }
        m_control.Move(m_movement*Time.deltaTime);
    }
    private void Unequip()
    {
        if (m_LeftWeapon != null)
        {
            m_LeftScript.Deequip();
            m_LeftScript = null;
            m_LeftWeapon.transform.position = transform.position;
            m_LeftWeapon = null;
            
        }
    }

    private void applyGravity()
    {
        if (!m_control.isGrounded)
        {
            m_movement.y = m_movement.y + (Physics.gravity.y * Time.deltaTime);
        }
        else
        {
            m_movement.y = 0;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        SocketWeapon sw = (SocketWeapon)other.gameObject.GetComponent<SocketWeapon>();
        if (sw != null)
            pickUpWeapon(sw);
    }
    public void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }
    public void OnTriggerExit(Collider other)
    {
        OnTriggerEnter(other);
    }

    protected virtual void pickUpWeapon(SocketWeapon newWeapon)
    {
        if (Input.GetKey(m_LeftEquipKey))
        {
            if ((m_LeftWeapon != null)&&(m_LeftScript!=newWeapon))
            {
                Unequip();
            }
            newWeapon.gameObject.transform.position = m_socketLoc.transform.position;
            newWeapon.Equip(gameObject);
            m_LeftScript = newWeapon;
            m_LeftWeapon = newWeapon.gameObject;
        }
    }
}
