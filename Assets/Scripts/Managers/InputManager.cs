using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static event Action MoveLeft = delegate { };
    public static event Action MoveRight = delegate { };
    public static event Action Shoot = delegate { };

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
            Shoot();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
            Shoot();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }

        if (Input.GetMouseButton(0))
        {
            if (Input.GetAxis("Mouse X") > 0.1f)
            {
                MoveRight();
                Shoot();
            }
            else if(Input.GetAxis("Mouse X") < -0.1f)
            {
                MoveLeft();
                Shoot();
            }
            else Shoot();
        }
    }
}
