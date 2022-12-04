using Manasoup.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


namespace Manasoup.Character
{
    public class CharacterCombat : MonoBehaviour
    {


        public static event Action DamageDone;
        [SerializeField]
        private int _damage;

        [SerializeField]
        private float _cooldown;
        [SerializeField]
        private List<DirectionToTransform> directionToTransforms;

        [SerializeField]
        private float _timeToShoot;

        [SerializeField]
        private float _attackRange;
        [SerializeField]
        private LayerMask _targetLayers;

        [SerializeField]
        private CharacterBase _character;
        private float _timer;

        public Direction currentDirection;

        public bool HitTriggered;

        void Start()
        {
            _timer = _cooldown;
        }

        private void Update()
        {
            if (_character._isDead)
                return;

            _timer+= Time.deltaTime;
            if (_character._hitTriggered && _timer >= _cooldown)
                StartCoroutine(Hit());
        }

        private IEnumerator Hit()
        {
            _character._animator.SetTrigger("Attack");

            _timer = 0;
            yield return new WaitForSeconds(_timeToShoot);
            var hitEnemies = Physics2D.OverlapCircleAll(GetAttackPoint(_character._direction).position, _attackRange, _targetLayers);
            foreach (var e in hitEnemies)
            {
                e.GetComponent<IDamageable>().TakeDamage(_damage);
                DamageDone?.Invoke();
            }
        }
        private void OnDrawGizmosSelected()
        {
            for (int i = 0; i < directionToTransforms.Count; i++)
            {
                Gizmos.DrawWireSphere(directionToTransforms[i].attackPoint.position, _attackRange);
            }
        }

        private Transform GetAttackPoint(Direction dir)
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