using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankControl : MonoBehaviour
{
    [SerializeField] private GameObject _rocketPrefab;
    [SerializeField] private Transform _rocketSpawnPoint;

    private float _shootTimerCurrentValue = 0f;
    private bool _shootLocked = false;

    private void OnEnable()
    {
        InputManager.MoveLeft += MoveLeft;
        InputManager.MoveRight += MoveRight;
        InputManager.Shoot += Shoot;
    }

    private void OnDisable()
    {
        InputManager.MoveLeft -= MoveLeft;
        InputManager.MoveRight -= MoveRight;
        InputManager.Shoot -= Shoot;
    }

    private void Update()
    {
        if (_shootLocked)
        {
            if (_shootTimerCurrentValue <= 1f / GameManager.FireSpeed)
            {
                _shootTimerCurrentValue += Time.deltaTime;
            }
            else
            {
                _shootLocked = false;
                _shootTimerCurrentValue = 0;
            }
        }

    }

    private void MoveRight()
    {
        var tankRotation = transform.rotation;
        tankRotation.eulerAngles = new Vector3(tankRotation.eulerAngles.x,
            tankRotation.eulerAngles.y + GameManager.RotationSpeed * Time.deltaTime, tankRotation.eulerAngles.z);
        transform.rotation = tankRotation;
    }

    private void MoveLeft()
    {
        var tankRotation = transform.rotation;
        tankRotation.eulerAngles = new Vector3(tankRotation.eulerAngles.x,
            tankRotation.eulerAngles.y - GameManager.RotationSpeed * Time.deltaTime, tankRotation.eulerAngles.z);
        transform.rotation = tankRotation;
    }

    private void Shoot()
    {
        if (_shootLocked) return;
        _shootLocked = true;

        var instRocket = Instantiate(_rocketPrefab, transform);
        instRocket.transform.localPosition = _rocketSpawnPoint.localPosition;
        instRocket.transform.localRotation = _rocketSpawnPoint.localRotation;
        instRocket.transform.parent = null;

        var rocketComponent = instRocket.GetComponent<Rocket>();
        rocketComponent.Launch();
    }

    public void Die()
    {
        SceneManager.LoadScene(0);
    }
}

