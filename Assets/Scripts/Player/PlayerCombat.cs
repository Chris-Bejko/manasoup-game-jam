using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manasoup.Interfaces;

namespace Manasoup
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField]
        private int _damage;

        [SerializeField]
        private float _cooldown;
        [SerializeField]
        private List<DirectionToTransform> directionToTransforms;

        [System.Serializable]
        public class DirectionToTransform
        {
            public PlayerDirection dir;
            public Transform attackPoint;
        }
        [SerializeField]
        private float _timeToShoot;

        [SerializeField]
        private float _attackRange;
        [SerializeField]
        private LayerMask _enemyLayers;

        [SerializeField]
        private PlayerBase _player;
        private float _timer;

        public PlayerDirection currentDirection;

        void Start()
        {
            _timer = _cooldown;
        }

        void Update()
        {
            GetInput();
        }


        private void GetInput()
        {
            if (_player._playerMovement._speed > 0 && !_player.canAttackWhileShooting)
                return;

            _timer += Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && _timer >= _cooldown)
            {
                StartCoroutine(Hit());
            }

        }


        private IEnumerator Hit()
        {
            _player._animator.SetTrigger("Attack");

            yield return new WaitForSeconds(_timeToShoot);
            _timer = 0;
            var hitEnemies = Physics2D.OverlapCircleAll(GetAttackPoint(currentDirection).position, _attackRange, _enemyLayers);
            foreach (var e in hitEnemies)
            {
                e.GetComponent<IDamageable>().TakeDamage(_damage);
            }
        }

        private void OnDrawGizmosSelected()
        {
            for (int i = 0; i < directionToTransforms.Count; i++)
            {
                Gizmos.DrawWireSphere(directionToTransforms[i].attackPoint.position, _attackRange);
            }
        }

        private Transform GetAttackPoint(PlayerDirection dir)
        {
            foreach (var e in directionToTransforms)
            {
                if (e.dir == dir)
                    return e.attackPoint;
            }
            return null;
        }
    }
}