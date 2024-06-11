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

    private float _currentVelocity;

    [SerializeField] private float _speed;
    [SerializeField] private float _turnSmoothing;


    void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    public void GetInput(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0, _input.y);
    }

    public void Move()
    {
        if(_input.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(_input.x, _input.y) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, _turnSmoothing);
        transform.rotation = Quaternion.Euler(0, angle, 0);

        _controller.Move(_direction * _speed * Time.deltaTime);
    }
}
