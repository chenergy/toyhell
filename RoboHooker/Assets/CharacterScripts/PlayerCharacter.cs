using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : MonoBehaviour {

    public float m_movementSpeed;
    public float m_jumpSpeed;
    public float m_turnSpeed;

    public KeyCode m_LeftKey;
    public KeyCode m_RightKey;
    public KeyCode m_JumpKey;

    public gamepad m_player;

    public Weapon m_mainWeaponScript;
    public GameObject m_socketLoc;
    public GameObject m_mainWeapon;
    public GameObject m_LeftWeapon;
    public SocketWeapon m_LeftScript;

    public AnimationClip m_spawn;
    public AnimationClip m_run;
    public AnimationClip m_primaryFire;
    public AnimationClip m_socketFire;
    public AnimationClip m_jump;
    public AnimationClip m_idle;
    public AnimationClip m_hurt;

    protected GamePadManager m_controller;
    protected Vector3 m_movement;
    private CharacterController m_control;
    private float m_zPosition;
	private bool m_gravityOn = true;
    
    // Use this for initialization
    void Start()
    {
        m_zPosition = transform.position.z;
        m_control = (CharacterController)GetComponent<CharacterController>();
        m_movement = new Vector3();
        m_controller = new GamePadManager(m_player);
        PlayClip(m_spawn, WrapMode.Once);
    }
    // Update is called once per frame
    public virtual void Update()
    {
        float m_Movedir = Input.GetAxis(m_controller.m_MoveAxisX);
        float m_fire = Input.GetAxis(m_controller.m_Attack);

        Vector2 m_Aim = new Vector2(Input.GetAxis(m_controller.m_AimAxisX), Input.GetAxis(m_controller.m_AimAxisY));
        if (m_fire != 0)
        {
            if (m_fire > 0)
            {
                PlayClip(m_primaryFire,WrapMode.Once);
                m_mainWeaponScript.fire(m_Aim);
            }
            else if (m_LeftWeapon != null)
            {
                PlayClip(m_socketFire, WrapMode.Once);
                m_LeftScript.fire(m_Aim);
            }
        }
        Debug.Log("Dir " + m_Movedir + " cur " + transform.forward.x);
        if ((m_Movedir != transform.forward.x) && m_Movedir != 0)
        {
            Debug.Log(m_Movedir+ " " + transform.forward);
            float rot = m_turnSpeed * Time.deltaTime;
            float maxRot = Vector2.Angle(new Vector2(m_Movedir, 0), new Vector2(transform.forward.x, transform.forward.z));
            if (rot > maxRot)
            {
                rot = maxRot;
            }
            Debug.Log(rot +" max "+ maxRot);
            if (m_Movedir < 0 && transform.forward.x > 0)
            {
                transform.Rotate(transform.up, rot);
            }
            else
            {
                transform.Rotate(transform.up, rot* (1));
            }
        }
            
        m_movement.x = m_movementSpeed * m_Movedir;

        if (m_gravityOn) 
            applyGravity(); // Added to modify gravity when climbing ladders
        
		if ((Input.GetKey(m_JumpKey) || Input.GetButton(m_controller.m_jumpButton)) && m_control.isGrounded)
        {
            Debug.Log("jump");
            PlayClip(m_jump, WrapMode.Loop);
            m_movement.y = m_jumpSpeed;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, m_zPosition);
        if (m_movement.magnitude > 1)
            PlayClip(m_run, WrapMode.Loop);
        else
            PlayClip(m_idle, WrapMode.Loop);
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

    private void pickUpWeapon(SocketWeapon newWeapon)
    {
        if (Input.GetButtonDown(m_controller.m_LeftEquipKey))
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
    private void PlayClip(AnimationClip ac, WrapMode mode)
    {
        if (ac != null)
        {
            if (!animation.IsPlaying(ac.name))
            {
                animation.wrapMode = mode;
                animation.Play(ac.name);
            }
        }
    }

    public bool Climbing
    {
        set
        {
            m_gravityOn = value;
        }
    }
}
