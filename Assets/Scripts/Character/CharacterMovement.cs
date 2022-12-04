using Manasoup.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Manasoup.Character
{
    public class CharacterMovement : MonoBehaviour
    {

        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private float _moveLimiter;
        [SerializeField]
        private Rigidbody2D _rb;
        [SerializeField]
        private CharacterBase _character;



        private void Update()
        {
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (_character._isPlayer)
                MovePlayer();
            else
                MoveAI();
        }

        private void MovePlayer()
        {
            if (_character._horizontal != 0 && _character._vertical != 0)
            {
                _character._horizontal *= _moveLimiter;
                _character._vertical *= _moveLimiter;
            }
            _character._isMoving = _character._horizontal != 0 || _character._vertical != 0;
            _character._speed = new Vector2(_character._horizontal, _character._vertical).sqrMagnitude;
            _character.SetDirection();
            _character.Animate();
            _rb.velocity = new Vector2(_character._horizontal * _moveSpeed * Time.fixedDeltaTime, _character._vertical * _moveSpeed * Time.fixedDeltaTime);
        }

        private void MoveAI()
        {
            if (!_character._isMoving)
                return;

            _character._dir = GameManager.Instance.player.transform.position - transform.position;
            _character._dir.Normalize();
            _rb.MovePosition(transform.position + (_moveSpeed * Time.deltaTime * _character._dir));
        }


        private void SetDir()
        {


        }


    }
}