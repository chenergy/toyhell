using UnityEngine;
using System.Collections;

public enum gamepad { one, two };
public class GamePadManager
{
    public string m_LeftEquipKey;
    public string m_Attack;
    public string m_jumpButton;
    public string m_MoveAxisX;
    public string m_MoveAxisY;
    public string m_AimAxisX;
    public string m_AimAxisY;
    public string m_activate;

    public GamePadManager(gamepad controller)
    {
        switch (controller)
        {
            case gamepad.one:
                m_LeftEquipKey = "P1equip";
                m_Attack = "P1fire";
                m_jumpButton = "P1jump";
                m_MoveAxisX = "P1moveX";
                m_MoveAxisY = "P1moveY";
                m_AimAxisX = "P1aimX";
                m_AimAxisY = "P1aimY";
                m_activate = "P1activate";
                break;
            case gamepad.two:
                m_LeftEquipKey = "P2equip";
                m_Attack = "P2fire";
                m_jumpButton = "P2jump";
                m_MoveAxisX = "P2moveX";
                m_MoveAxisY = "P2moveY";
                m_AimAxisX = "P2aimX";
                m_AimAxisY = "P2aimY";
                m_activate = "P2activate";
                break;
        }
    }
    public static bool Equip
    {
        get
        {
            if (Input.GetButton("P1equip") || Input.GetButton("P2equip"))
                return true;
            else
                return false;
        }
    }
    public static bool Attack{
        get
        {
            if (Input.GetAxis("P2fire") != 0 || Input.GetAxis("P1fire") != 0)
                return true;
            else
                return false;
        }
}
    public static bool Jump
    {
        get
        {
            if (Input.GetButton("P1jump") || Input.GetButton("P2jump"))
                return true;
            else
                return false;
        }
    }
    public static bool Activate
    {
        get
        {
            if (Input.GetButton("P2activate") || Input.GetButton("P1activate"))
                return true;
            else
                return false;
        }

    }
    
}