using Manasoup.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


namespace Manasoup.Character
{
    public class CharacterCombat : MonoBehaviour
    {

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
            _timer++;
            if (_character._hitTriggered)
                StartCoroutine(Hit());
        }

        private IEnumerator Hit()
        {
            _character._animator.SetTrigger("Attack");

            yield return new WaitForSeconds(_timeToShoot);
            _timer = 0;
            var hitEnemies = Physics2D.OverlapCircleAll(GetAttackPoint(currentDirection).position, _attackRange, _targetLayers);
            foreach (var e in hitEnemies)
            {
                e.GetComponent<IDamageable>().TakeDamage(_damage);
            }
            _character._hitTriggered = false;
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