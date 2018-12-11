using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaragesManager : MonoBehaviour
{ 
    private static GaragesManager _instance;

    private GarageControl[] _garages;
    public static GarageControl[] Garages
    {
        get
        {
            if (_instance._garages == null) _instance._garages = _instance.transform.GetComponentsInChildren<GarageControl>();
            return _instance._garages;
        }
    }

    private float _enemySpawnTimerCurrentValue = 0f;
    private float _enemySpawnTimerValue = 4f;
    private float _enemySpawnTimerMinValue = 2f;
    private float _enemySpawnTimerMaxValue = 4f;

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (_enemySpawnTimerCurrentValue <= _enemySpawnTimerValue)
        {
            _enemySpawnTimerCurrentValue += Time.deltaTime;
        }
        else
        {
            Garages[Random.Range(0,Garages.Length)].SpawnEnemy();
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        _enemySpawnTimerCurrentValue = 0f;
        _enemySpawnTimerValue = Random.Range(_enemySpawnTimerMinValue, _enemySpawnTimerMaxValue);
    }
}
