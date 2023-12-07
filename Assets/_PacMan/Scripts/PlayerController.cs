using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _tpsCamera;
    [SerializeField] private float _powerupDuration;
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private int _health;
    [SerializeField] private TMP_Text _heatlhText;
    
    private Rigidbody _rigidbody;
    private Vector3 _movementDir;
    private Coroutine _powerupCoroutine;
    private bool _isPowerUpActive;

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;
    
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        UpdateUI();
        
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

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPowerUpActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    public void Dead()
    {
        _health -= 1;
        if (_health > 0)
        {
            transform.position = _respawnPoint.position;
        }
        else
        {
            _health = 0;
            Debug.Log("Lose");
        }
        UpdateUI();
    }
    
    public void UpdateUI()
    {
        _heatlhText.text = "Score: " + _health;
    }
    
    private IEnumerator StartPowerUp()
    {
        Debug.Log("Start Power Up");
        _isPowerUpActive = true;
        if (OnPowerUpStart != null) OnPowerUpStart();
        yield return new WaitForSeconds(_powerupDuration);
        
        _isPowerUpActive = false;
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
