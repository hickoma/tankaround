using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private TankControl _tankControl;
    public static TankControl TankControl
    {
        get { return _instance._tankControl; }
    }

    [SerializeField] private float _rotationSpeed;
    public static float RotationSpeed
    {
        get { return _instance._rotationSpeed; }
        private set { _instance._rotationSpeed = value; }
    }

    [SerializeField] private float _fireSpeed = 10f;
    public static float FireSpeed
    {
        get { return _instance._fireSpeed; }
        set { _instance._fireSpeed = value; }
    }

    [SerializeField] private float _fireDamage = 10f;
    public static float FireDamage
    {
        get { return _instance._fireDamage; }
        set { _instance._fireDamage = value; }
    }

    [SerializeField] private float _fireRate = 2f;
    public static float FireRate
    {
        get { return _instance._fireRate; }
        set { _instance._fireRate = value; }
    }

    [SerializeField] private float _enemySpeed = 1f;
    public static float EnemySpeed
    {
        get { return _instance._enemySpeed; }
        set { _instance._enemySpeed = value; }
    }

    [SerializeField] private float _enemyHealth = 20f;
    public static float EnemyHealth
    {
        get { return _instance._enemyHealth; }
        set { _instance._enemyHealth = value; }
    }

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }
}
