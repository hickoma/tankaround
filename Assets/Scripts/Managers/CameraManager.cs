using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;

    [SerializeField] private float _yOffset = 9.5f;
    [SerializeField] private float _zOffset = -10f;

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    private void Update()
    {
        transform.localPosition = new Vector3(0f, _yOffset, _zOffset);
    }
}
