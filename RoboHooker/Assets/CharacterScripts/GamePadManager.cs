using UnityEngine;
using System.Collections;

public enum gamepad { one, two };
public class GamePadManager {
        public string m_LeftEquipKey;
    public string m_Attack;
    public string m_jumpButton;
    public string m_MoveAxisX;
    public string m_MoveAxisY;
    public string m_AimAxisX;
    public string m_AimAxisY;

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
                break;
            case gamepad.two:
                m_LeftEquipKey = "P2equip";
                m_Attack = "P2fire";
                m_jumpButton = "P2jump";
                m_MoveAxisX = "P2moveX";
                m_MoveAxisY = "P2moveY";
                m_AimAxisX = "P2aimX";
                m_AimAxisY = "P2aimY";
                break;
        }

    }
}
