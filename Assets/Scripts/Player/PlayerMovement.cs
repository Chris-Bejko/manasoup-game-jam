using UnityEngine;

namespace Manasoup
{
    public class PlayerMovement : PlayerBase
    {

        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private float _moveLimiter;
        [SerializeField]
        private Rigidbody2D _rb;

        private float _horizontal, _vertical;
        private PlayerCombat thisCombat;
        private void OnEnable()
        {
            GameManager.OnGameStateChanged += OnStateChange;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= OnStateChange;
        }

        private void FixedUpdate()
        {
            if (isCombating)
                return;

            Move();
        }

        private void Move()
        {
            if (_horizontal != 0 && _vertical != 0)
            {
                _horizontal *= _moveLimiter;
                _vertical *= _moveLimiter;
            }
            isMoving = _horizontal != 0 && _vertical != 0;
            SetDir();

            _rb.velocity = new Vector2(_horizontal * _moveSpeed, _vertical * _moveSpeed);
        }

        // Update is called once per frame
        void Update()
        {
            GetInput();
        }

        private void OnStateChange(GameManager.GameState state)
        {
            gameObject.SetActive(state == GameManager.GameState.Playing);


        }

        private void GetInput()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");

        }

        private void SetDir()
        {
            if (_vertical < 0)
                thisCombat.currentDirection = PlayerDirection.Down;
            if (_vertical > 0)
                thisCombat.currentDirection = PlayerDirection.Up;
            if (_horizontal > 0)
                thisCombat.currentDirection = PlayerDirection.Right;
            if (_horizontal < 0)
                thisCombat.currentDirection = PlayerDirection.Left;
        }
    }
}