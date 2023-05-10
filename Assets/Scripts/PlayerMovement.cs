using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float _extraJumpValue;
    [SerializeField] private float _checkRadius;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveInput;
    private float _extraJump;
    private float _speed = 5f;
    private bool _isGround;


    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Jump();
        Move();
    }

    private void Move()
    {
        _moveInput.x = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = new Vector2(_moveInput.x * _speed, _rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        if (_isGround == true)
        {
            _extraJump = _extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _extraJump > 0)
        {
            _rigidbody2D.velocity = Vector2.up * _extraJump;
            _extraJump--;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && _extraJump == 0 && _isGround == true)
        {
            _rigidbody2D.velocity = Vector2.up * _jumpForce;
        }

        _isGround = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);
    }
}