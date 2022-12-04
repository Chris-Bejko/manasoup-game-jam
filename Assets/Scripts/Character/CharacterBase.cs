using Manasoup.Interfaces;
using System.Collections;
using UnityEngine;


namespace Manasoup.Character
{
    public class CharacterBase : MonoBehaviour , IDamageable
    {
        public bool _isPlayer;

        public bool _isMoving;

        public bool _canMove;

        public bool _isCombating;

        public int _currentRoom;

        public Animator _animator;

        public bool _canAttackWhileShooting;

        public bool _isDead;
        public int Health { get; set; }

        public int MaxHealth;

        public Direction _direction;

        public float _horizontal, _vertical, _speed;

        public SpriteRenderer _spriteRenderer;

        public bool _hitTriggered;


        public float _minDistance;

        public float _combatDistance;

        public Vector3 _initialPosition;
        public Vector3 _dir;
        public void OnEnable()
        {
            GameManager.OnGameStateChanged += OnStateChange;
        }

        public void OnDisable()
        {
            GameManager.OnGameStateChanged -= OnStateChange;
        }

        public void Update()
        {
            GetInput();
            Animate();
        }

        public void Init()
        {
            transform.position = _initialPosition;
            Health = MaxHealth;
        }
        private void GetInput()
        {
            if (_isPlayer)
                GetPlayerInput();
            else
                GetAIInput();
        }

        private void GetPlayerInput()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");
            if (Input.GetKeyDown(KeyCode.Space))
                _hitTriggered = true;
            else
                _hitTriggered = false;
                
        }

        private void GetAIInput()
        {
            _isMoving = false;
            var distanceFromPlayer = Vector2.Distance(GameManager.Instance.player.transform.position, gameObject.transform.position);
            if (distanceFromPlayer < _minDistance)
                return;

            if (GameManager.Instance.player._currentRoom != _currentRoom)
                return;

            _isMoving = true;
            _hitTriggered = distanceFromPlayer <= _combatDistance;
            SetDirection();
        }
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if(_isPlayer)
                Debug.LogError(Health);
            if (Health <= 0)
            {
                _isDead = true;
                Die();
                if(!_isPlayer)
                    GameManager.Instance.enemiesManager.CheckEnemiesLeft();
            }
        }

        public void Die()
        {
            StartCoroutine(IDie());
            if (_isPlayer)
                GameManager.Instance.ChangeState(GameManager.GameState.Lost);
        }
        private IEnumerator IDie()
        {
            _animator.SetTrigger("Die");
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
        public void Animate()
        {
            _animator.SetFloat("Direction", (int)_direction);
            if (_isPlayer)
                AnimatePlayer();
            else
                _animator.SetBool("IsMoving", _isMoving);

        }

        private void AnimatePlayer()
        {
            _animator.SetFloat("Speed", _speed);
            _animator.SetFloat("Horizontal", _horizontal);
            _animator.SetFloat("Vertical", _vertical);
        }

        public  void SetDirection()
        {
            if (_isPlayer)
                SetPlayerDirection();
            else
                SetAIDirection();
        }

        private void OnStateChange(GameManager.GameState state)
        {
            Debug.LogError(state);
            gameObject.SetActive(state == GameManager.GameState.Playing);
            if (state == GameManager.GameState.Playing)
                Init();

        }

        private void SetPlayerDirection()
        {
            if (_vertical < 0)
                _direction = Direction.Down;
            if (_vertical > 0)
                _direction = Direction.Up;
            if (_horizontal > 0)
                _direction = Direction.Right;
            if (_horizontal < 0)
                _direction = Direction.Left;
        }

        private void SetAIDirection()
        {
            if (_dir.y > 0)
                _direction = Direction.Up;
            if (_dir.y < 0)
                _direction = Direction.Down;
            if (_dir.x < -.3)
                _direction = Direction.Left;
            if (_dir.x > .3)
                _direction = Direction.Right;


            ///Sprite order to handle when player is above enemy
            _spriteRenderer.sortingOrder = _dir.y > 0.3 ? 6 : 4;
        }

        public void Heal()
        {
        }

        public void Heal(int health)
        {
        }
    }
}