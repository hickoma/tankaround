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

        if (_enemyHealth<=0)
        {
            Die();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player")) GameManager.TankControl.Die();

        if (other.gameObject.CompareTag("Rocket"))
        {
            _enemyHealth -= GameManager.FireDamage;
            Destroy(other.gameObject);
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
