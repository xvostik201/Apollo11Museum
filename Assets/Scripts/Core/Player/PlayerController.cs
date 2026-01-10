using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move Settings")] 
    [SerializeField]
    private float _moveSpeed = 5f;

    private float _verticalVelocity;
    
    [Header("Gravity")]
    [SerializeField] private float _gravity = -9.8f;
    
    [Header("Camera Settings")]
    [SerializeField] private Camera _camera;
    [SerializeField] private float _sensivity = 5f;
    [SerializeField] private float _clampY = 85f;

    private float _pitchY = 0f;
    
    [Header("Interaction")]
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private float _rayDistance = 5f;
    
    private CharacterController _characterController;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        
        _characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Look();
        Move();
        HandleInteraction();
    }

    private void Look()
    {
        float mX = Input.GetAxis("Mouse X") * _sensivity;
        float mY = Input.GetAxis("Mouse Y") * _sensivity;

        _pitchY -= mY;
        _pitchY = Mathf.Clamp(_pitchY, -_clampY, _clampY);
        
        _camera.transform.localRotation = Quaternion.Euler(_pitchY, 0, 0f);
        transform.Rotate(Vector3.up * mX);
    }

    private void Move()
    {
        if (_characterController.isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -2f;
        }
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        Vector3 moveDirection = (transform.forward * v + transform.right * h).normalized;
        _verticalVelocity += _gravity * Time.deltaTime;
        Vector3 finalVelocity = moveDirection * _moveSpeed;
        finalVelocity.y = _verticalVelocity;
        
        _characterController.Move(finalVelocity * Time.deltaTime);
    }

    private void HandleInteraction()
    {
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _rayDistance, Color.red);
        
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _rayDistance, _interactionLayer))
        {
            if (hit.collider.TryGetComponent(out Button interactionButton))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    interactionButton.PressButton();
                }
            }
        }
    }
}
