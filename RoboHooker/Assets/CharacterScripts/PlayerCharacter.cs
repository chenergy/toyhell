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
    public KeyCode m_LeftEquipKey;
    public string m_jumpButton;
    public string m_MoveAxis;
    public string m_AimAxisX;
    public string m_AimAxisY;

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
    void Update()
    {
        float m_Movedir = Input.GetAxis(m_MoveAxis);
        Vector2 m_Aim = new Vector2(Input.GetAxis(m_AimAxisX), Input.GetAxis(m_AimAxisY));
        Debug.Log(m_Aim + ", " + m_Movedir);
        m_movement.x = m_movementSpeed * m_Movedir;
        applyGravity();
        if ((Input.GetKey(m_JumpKey)||Input.GetKey(m_jumpButton)) && m_control.isGrounded)
        {
            Debug.Log("jump");
            m_movement.y = m_jumpSpeed;
        }
        m_control.Move(m_movement*Time.deltaTime);
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
    protected virtual void pickUpWeapon(SocketWeapon newWeapon)
    {
        if (Input.GetKey(m_LeftEquipKey))
        {
            m_LeftScript = newWeapon;
            m_LeftWeapon = newWeapon.gameObject;
        }
    }
}
