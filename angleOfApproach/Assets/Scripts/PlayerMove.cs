using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    private Vector2 _input;
    private Vector3 _direction;
    private CharacterController _controller;

    private Animator _animator;
    public float animX = 0;
    public float animY = 0;

    private float _currentVelocity;

    private float _speed;
    [SerializeField] private float _walkSpeed = 2.0f;
    [SerializeField] private float _sprintSpeed = 5.0f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _turnSmoothing;


    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        // _speed = _walkSpeed;
        _speed = _sprintSpeed;
    }

    void Update()
    {
        // ApplyRotation();
        Move();
    }

    //Function to get input from player using the New Input System
    public void GetInput(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0, _input.y);

        if(context.ReadValue<Vector2>() != Vector2.zero)
        {
            animX = _input.x;
            animY = _input.y;
            _animator.SetFloat("Horizontal", animX);
            _animator.SetFloat("Vertical", animY);
        }

        if(context.ReadValue<Vector2>() == Vector2.zero)
        {
            animX = 0;
            animY = 0;
            _animator.SetFloat("Horizontal", animX);
            _animator.SetFloat("Vertical", animY);
        }

    }

    // public void Sprint(InputAction.CallbackContext context)
    // {
    //     if(context.performed)
    //     {
    //         _speed = _sprintSpeed;
    //     }
    //     if(context.canceled)
    //     {
    //         _speed = _walkSpeed;
    //     }
    // }

    void ApplyRotation()
    {
        if(_input.sqrMagnitude == 0) return;

        Debug.Log("Working");
        //Calculations for turning
        var targetAngle = Mathf.Atan2(_input.x, _input.y) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, _turnSmoothing);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    //Function which calculates player movement
    void Move()
    {
        _controller.Move(_direction * _speed * Time.deltaTime);
    }

    void TriggerAnims()
    {

    }
}
