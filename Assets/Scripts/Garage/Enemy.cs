using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _enemyHealth;
    private float _enemySpeed;
    private bool _launched = false;

    private float _selfDestructTimerCurrentValue = 0f;
    private float _selfDestructTimerDefaultValue = 15f;

    private GameObject _currentEnemyArrow;
    private bool _needShowArrow = true;
    private Side _enemySideIfInvisible = Side.None;
    private bool _isVisible = false;

    private void Start()
    {
        _enemyHealth = GameManager.EnemyHealth;
        _enemySpeed = GameManager.EnemySpeed;
    }

    private void Update()
    {
        if (_selfDestructTimerCurrentValue <= _selfDestructTimerDefaultValue)
            _selfDestructTimerCurrentValue += Time.deltaTime;
        else Destroy(gameObject);

        if (_launched)
        {
            transform.position += transform.forward * _enemySpeed * Time.deltaTime;
        }

        var screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        _isVisible = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (GameManager.TankControl && !_isVisible && _needShowArrow)
        {
            var prevSide = _enemySideIfInvisible;
            var enemyVector = new Vector3(transform.position.x, transform.position.z);
            var tankVector = new Vector3(GameManager.TankControl.transform.forward.x, GameManager.TankControl.transform.forward.z);

            var crossProduct = Vector3.Cross(tankVector, enemyVector);

            _enemySideIfInvisible = Mathf.Sign(crossProduct.z) < 0 ? Side.Right : Side.Left;

            if (_enemySideIfInvisible != prevSide)
            {
                if (_currentEnemyArrow) Destroy(_currentEnemyArrow);
                _currentEnemyArrow = GaragesManager.SpawnEnemyArrow(_enemySideIfInvisible);
            }
        }
        else
        {
            _enemySideIfInvisible = Side.None;
            if (_currentEnemyArrow) Destroy(_currentEnemyArrow);
        }

        if (_enemyHealth<=0)
        {
            Die();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) GameManager.TankControl.Die();

        if (other.gameObject.CompareTag("Rocket"))
        {
            _enemyHealth -= GameManager.FireDamage;
            Destroy(other.gameObject);
        }
    }
    
    private void OnDestroy()
    {
        if (_currentEnemyArrow)
        {
            _needShowArrow = false;
            Destroy(_currentEnemyArrow);
        }
    }

    public void Launch()
    {
        _launched = true;
    }

    public void Die()
    {
        ScoreManager.Score++;
        Destroy(gameObject);
    }
}
