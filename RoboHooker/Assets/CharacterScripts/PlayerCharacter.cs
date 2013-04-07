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

    public GameObject m_LeftWeapon;
    public SocketedWeapon m_LeftScript;

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
        if (Input.GetKey(m_LeftKey))
            m_movement.x = m_movementSpeed * (-1);
        else if (Input.GetKey(m_RightKey))
            m_movement.x = m_movementSpeed;
        else
            m_movement.x = 0;

        applyGravity();
    
        if (Input.GetKey(m_JumpKey)&&m_control.isGrounded)
            m_movement.y = m_jumpSpeed;
        m_control.Move(m_movement*Time.deltaTime);
    }

    private void applyGravity()
    {
        if (!m_control.isGrounded)
        {
            m_movement = m_movement + (Physics.gravity * Time.deltaTime);
        }
        else
        {
            m_movement.y = 0;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
    }
    protected virtual void pickUpWeapon(SocketedWeapon newWeapon)
    {
    }
}
