using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageControl : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemySpawnPoint;

    public void SpawnEnemy()
    {
        var instEnemy = Instantiate(_enemyPrefab, transform);
        instEnemy.transform.localPosition = _enemySpawnPoint.localPosition;
        instEnemy.transform.parent = null;
        instEnemy.transform.localScale = Vector3.one * 2;
        instEnemy.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        instEnemy.transform.LookAt(GameManager.TankControl.transform);
        instEnemy.transform.rotation = Quaternion.Euler(0f,
            instEnemy.transform.rotation.eulerAngles.y, 0f);

        var enemyComponent = instEnemy.GetComponent<Enemy>();
        enemyComponent.Launch();
    }
}
