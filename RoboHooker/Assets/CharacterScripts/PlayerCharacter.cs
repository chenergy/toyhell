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
    public KeyCode m_FireSpecial;
    public KeyCode m_FireSocket;

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
    protected float m_fire;
    private CharacterController m_control;
    private float m_zPosition;
	private bool m_climbing = false;
	private bool m_frozen = false;
    
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
        float movedir = Input.GetAxis(m_controller.m_MoveAxisX);
        float fire = Input.GetAxis(m_controller.m_Attack);

        Vector2 aim = new Vector2(Input.GetAxis(m_controller.m_AimAxisX), Input.GetAxis(m_controller.m_AimAxisY));
        m_fire = Input.GetAxis(m_controller.m_Attack);

        if (Input.GetKey(m_LeftKey))
        {
            movedir = -1;
        }
        else if (Input.GetKey(m_RightKey))
        {
            movedir = 1;
        }
        if (Input.GetKey(m_FireSpecial))
        {
            fire = 1;
        }
        if (Input.GetKey(m_FireSocket))
        {
            fire = -1;
        }
        if (fire != 0)
        {
            if (fire > 0)
            {
                PlayClip(m_primaryFire,WrapMode.Once);
                m_mainWeaponScript.fire(aim);
            }
            else if (m_LeftWeapon != null)
            {
                PlayClip(m_socketFire, WrapMode.Once);
                m_LeftScript.fire(aim);
            }
        }
        if ((movedir != transform.forward.x) && movedir != 0)
        {
//            Debug.Log(movedir+ " " + transform.forward);
            float rot = m_turnSpeed * Time.deltaTime;
            float maxRot = Vector2.Angle(new Vector2(movedir, 0), new Vector2(transform.forward.x, transform.forward.z));
            if (rot > maxRot)
            {
                rot = maxRot;
            }
//            Debug.Log(rot +" max "+ maxRot);
            if (movedir < 0 && transform.forward.x > 0)
            {
                transform.Rotate(transform.up, (-1)*rot);
            }
            else
            {
                transform.Rotate(transform.up, rot* (1));
            }
        }
            
        m_movement.x = m_movementSpeed * movedir;

        if (!m_climbing){ 
            applyGravity(); // Added to modify gravity when climbing ladders
		}
		else{
			m_movement.y = 0;
		}
        
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
		
		if (m_frozen) { m_movement = Vector3.zero; }	//Added frozen attribute to control movement
		
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
        if ((ac != null)&&(animation!=null))
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
            m_climbing = value;
        }
		get { return m_climbing; }
    }
	
	public bool Frozen{
		get { return m_frozen; }
		set { m_frozen = value; }
	}
}
