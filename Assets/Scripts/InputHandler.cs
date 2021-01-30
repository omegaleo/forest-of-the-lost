using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public bool XBoxGamepadOn = false;
    public bool PS4GamepadOn = false;

    private void Update()
    {
        string[] names = Input.GetJoystickNames();
        XBoxGamepadOn = false;
        PS4GamepadOn = false;
        for (int x = 0; x < names.Length; x++)
        {
            print(names[x].Length);
            if (names[x].Length == 19)
            {
                PS4GamepadOn = true;
            }
            if (names[x].Length == 33)
            {
                XBoxGamepadOn = true;

            }
        }
    }


    public string GetKey(string key)
    {
        switch(key.ToUpper())
        {
            case "ACTION":
                if(!XBoxGamepadOn && !PS4GamepadOn)
                {
                    return "Z";
                }
                else if(XBoxGamepadOn)
                {
                    return "Y";
                }
                else if(PS4GamepadOn)
                {
                    return "\u9650";
                }
                break;
            case "JUMP":
                if (!XBoxGamepadOn && !PS4GamepadOn)
                {
                    return "SPACE";
                }
                else if (XBoxGamepadOn)
                {
                    return "A";
                }
                else if (PS4GamepadOn)
                {
                    return "X";
                }
                break;
        }

        return "";
    }
}
