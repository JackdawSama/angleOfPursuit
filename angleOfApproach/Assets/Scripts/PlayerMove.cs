using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    private Vector2 _input;
    private Vector3 _direction;
    private CharacterController _controller;

    [SerializeField] private float _speed;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        _controller.Move(_direction * _speed * Time.deltaTime);
    }
    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();

        _direction = new Vector3(_input.x, 0, _input.y);
        Debug.Log(_input);
    }
}
