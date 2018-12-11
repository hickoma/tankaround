using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static event Action<float> MoveLeft = delegate { };
    public static event Action<float> MoveRight = delegate { };
    public static event Action Shoot = delegate { };

    public LeanScreenDepth ScreenDepth;

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft(1);
            Shoot();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight(1);
            Shoot();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
    }

    protected virtual void LateUpdate()
    {
        var fingers = LeanTouch.GetFingers(false, false, 1);
        if (fingers.Count < 1) return;

        var lastScreenPoint = LeanGesture.GetLastScreenCenter(fingers);
        var screenPoint = LeanGesture.GetScreenCenter(fingers);

        var worldDelta = lastScreenPoint - screenPoint;

        if (worldDelta.x < 0)
        {
            MoveLeft(Mathf.Abs(worldDelta.x));
        }
        else if (worldDelta.x > 0)
        {
            MoveRight(Mathf.Abs(worldDelta.x));
        }
        Shoot();
    }
}
