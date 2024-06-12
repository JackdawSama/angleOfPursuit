using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{   
    //Input & Movement Variables
    private Vector2 _input;
    private Vector3 _direction;
    private CharacterController _controller;
    private float _speed;
    [SerializeField] private float _sprintSpeed = 5.0f;

    //Animator Variables
    private Animator _animator;
    float animX = 0;
    float animY = 0;

    //Misc Variables
    private bool inputKill = false;


    void Awake()
    {   
        //Caching Components and variables
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _speed = _sprintSpeed;
    }

    void Update()
    {
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

    //Function which calculates player movement
    void Move()
    {
        if(inputKill) return;

        _controller.Move(_direction * _speed * Time.deltaTime);
    }

    //Function to kill input
    void KillInput()
    {
        inputKill = true;
    }

    //Subscribing & Unsubscribing to Events
    void OnEnable()
    {
        TriggerSys.touchDown += KillInput;
        TriggerSys.outOfBounds += KillInput;
    }

    void OnDisable()
    {
        TriggerSys.touchDown -= KillInput;
        TriggerSys.outOfBounds -= KillInput;
    }
}
