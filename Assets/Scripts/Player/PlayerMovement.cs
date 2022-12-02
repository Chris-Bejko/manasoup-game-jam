using Manasoup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerBase
{

    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _moveLimiter;
    [SerializeField]
    private Rigidbody2D _rb;

    private float _horizontal, _vertical;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += OnStateChange;
    }

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (isCombating)
            return;

        if (_horizontal != 0 && _vertical != 0) 
        {
            _horizontal *= _moveLimiter;
            _vertical *= _moveLimiter;
        }

        isMoving = true;

        _rb.velocity = new Vector2(_horizontal * _moveSpeed, _vertical * _moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void OnStateChange(GameState state)
    {
        gameObject.SetActive(state == GameState.Playing);


    }

    private void GetInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }
}
