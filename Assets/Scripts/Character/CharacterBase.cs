using Manasoup.Interfaces;
using System;
using System.Collections;
using UnityEngine;


namespace Manasoup.Character
{
    public class CharacterBase : MonoBehaviour , IDamageable
    {
        public bool _isPlayer;

        public bool _isMoving;


        public bool _isBoss;

        public bool _canMove;

        public bool _isCombating;

        public int _currentRoom;

        public Animator _animator;

        public bool _canAttackWhileShooting;

        public AudioSource _source;

        public bool _isDead;
        public int Health { get; set; }

        public int MaxHealth;

        public Direction _direction;

        public float _horizontal, _vertical, _speed;

        public SpriteRenderer _spriteRenderer;

        public bool _hitTriggered;


        public float _minDistance;

        public float _combatDistance;

        public Transform _initialPosition;
        public Vector3 _dir;

        public GameObject heartPefab;
        public static event Action HealingDone;
        public static event Action OnBossDied;

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
            if (_isPlayer)
                WalkSound();
        }

        private void WalkSound()
        {
            if(_horizontal != 0 || _vertical != 0)
                _isMoving = true;
            else
                _isMoving = false;

            if(_isMoving && !_source.isPlaying)
                _source.Play();

            if (!_isMoving)
                _source.Stop();
        }
        public void Init()
        {
            gameObject.SetActive(true);
            transform.position = _initialPosition.position;
            Health = MaxHealth;
            if (_isPlayer)
                _currentRoom = 0;

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
            if (_isBoss)
                OnBossDied?.Invoke();
            StartCoroutine(IDie());
            if (_isPlayer)
                GameManager.Instance.ChangeState(GameManager.GameState.Lost);

        }

        private IEnumerator DropHeart()
        {
            yield return new WaitForSeconds(1f);
            var heart = Instantiate(heartPefab);
            heart.transform.position = transform.position;
        }
        private IEnumerator IDie()
        {
            if (!_isPlayer)
                StartCoroutine(DropHeart());
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
            if (_dir.y > 0.1)
                _direction = Direction.Up;
            if (_dir.y < -.1)
                _direction = Direction.Down;
            if (_dir.x < -.1)
                _direction = Direction.Left;
            if (_dir.x > .1)
                _direction = Direction.Right;


            ///Sprite order to handle when player is above enemy
            _spriteRenderer.sortingOrder = _dir.y > 0.3 ? 6 : 4;
        }

        public void Heal(int health)
        {
            Health += health;
            HealingDone?.Invoke();
            
        }

        public void SetVolume(float volume)
        {
            _source.volume = volume;
        }
    }
}