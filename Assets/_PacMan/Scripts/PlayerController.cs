using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _tpsCamera;
    [SerializeField] private float _powerupDuration;
    private Rigidbody _rigidbody;
    private Vector3 _movementDir;
    private Coroutine _powerupCoroutine;

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
    }

    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDir = Horizontal * _tpsCamera.right;
        Vector3 verticalDir = Vertical * _tpsCamera.forward;

        horizontalDir.y = 0;
        verticalDir.y = 0;

        _movementDir = horizontalDir + verticalDir;
        _rigidbody.velocity = _movementDir * _speed * Time.deltaTime;
    }
    
    private IEnumerator StartPowerUp()
    {
        Debug.Log("Start Power Up");
        if (OnPowerUpStart != null) OnPowerUpStart();
        yield return new WaitForSeconds(_powerupDuration);
        if (OnPowerUpStop != null) OnPowerUpStop();
        Debug.Log("Stop Power Up");
    }
    
    public void PickPowerUp()
    {
        Debug.Log("Pick Power Up");
        
        if (_powerupCoroutine != null)
        {
            StopCoroutine(_powerupCoroutine);
        }
        _powerupCoroutine = StartCoroutine(StartPowerUp());
    }
}
