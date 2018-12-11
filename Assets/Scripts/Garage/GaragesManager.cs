using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side
{
    None,
    Left,
    Right
}

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

    [SerializeField] private GameObject _enemyArrowPrefab;
    [SerializeField] private Transform _leftEnemyArrowsContainer;
    [SerializeField] private Transform _rightEnemyArrowsContainer;

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

    public static GameObject SpawnEnemyArrow(Side side)
    {
        GameObject instantiatedArrow;

        switch (side)
        {
            case Side.Left:
                instantiatedArrow = Instantiate(_instance._enemyArrowPrefab, _instance._leftEnemyArrowsContainer);
                instantiatedArrow.transform.localScale = new Vector3(-1, 1, 1);
                break;
            case Side.Right:
                instantiatedArrow = Instantiate(_instance._enemyArrowPrefab, _instance._rightEnemyArrowsContainer);
                instantiatedArrow.transform.localScale = new Vector3(1, 1, 1);
                break;
            default:
                instantiatedArrow = Instantiate(_instance._enemyArrowPrefab, _instance._rightEnemyArrowsContainer);
                instantiatedArrow.transform.localScale = new Vector3(1, 1, 1);
                break;
        }

        return instantiatedArrow;
    }

    private void ResetTimer()
    {
        _enemySpawnTimerCurrentValue = 0f;
        _enemySpawnTimerValue = Random.Range(_enemySpawnTimerMinValue, _enemySpawnTimerMaxValue);
    }
}
