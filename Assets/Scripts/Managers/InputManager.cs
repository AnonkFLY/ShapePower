using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    private InputButton inputButton;

    public InputButton InputButton
    {
        get
        {
            if (inputButton == null)
            {
                inputButton = GameObject.FindObjectOfType<InputButton>();
            }
            return inputButton;
        }
    }
}
