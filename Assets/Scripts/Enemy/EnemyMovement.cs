using UnityEngine;
using Manasoup.Interfaces;
namespace Manasoup.AI
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField]
        private float _minDistance;
        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private EnemyBase _enemyBase;
        [SerializeField]
        private Direction EnemyDirection;

        private Vector3 dir;
        private bool isMoving;
        void Update()
        {
            GetInput();
        }

        private void FixedUpdate()
        {
            Chase();
        }

        private void GetInput()
        {
            isMoving = false;

            if (Vector2.Distance(GameManager.Instance.player.transform.position, gameObject.transform.position) < _minDistance)
                return;

            if (GameManager.Instance.player.currentRoom != _enemyBase._enemyRoom)
                return;

            isMoving = true;
            SetDirection();
            Chase();
            Animate();
        }

        private void Chase()
        {
            dir = GameManager.Instance.player.transform.position - transform.position;
            dir.Normalize();
            _enemyBase._thisRigidbody.MovePosition(transform.position +(_moveSpeed * Time.deltaTime * dir));
        }

        private void SetDirection()
        {
            if (dir.y == 1)
                EnemyDirection = Direction.Up;
            if(dir.y == -1)
                EnemyDirection = Direction.Down;
            if(dir.x == -1)
                EnemyDirection = Direction.Left;
            if(dir.x == 1)
                EnemyDirection = Direction.Right;
                
        }

        private void Animate()
        {
            Debug.LogError(isMoving);
            Debug.LogError(EnemyDirection);
            _enemyBase._animator.SetBool("IsMoving", isMoving);
            _enemyBase._animator.SetFloat("Direction", (int)EnemyDirection);
        }
    }
}

