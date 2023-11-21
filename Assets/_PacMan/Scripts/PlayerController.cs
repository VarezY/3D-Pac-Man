using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _tpsCamera;

    private Rigidbody _rigidbody;
    private Vector3 _movementDir;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDir = Horizontal * _tpsCamera.right;
        Vector3 verticalDir = Vertical * _tpsCamera.forward;

        horizontalDir.y = 0;
        verticalDir.y = 0;

        _movementDir = horizontalDir + verticalDir;
        _rigidbody.velocity = _movementDir * _speed * Time.fixedDeltaTime;
    }
}
