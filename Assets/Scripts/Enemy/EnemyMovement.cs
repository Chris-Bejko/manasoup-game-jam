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

        void Update()
        {
            GetInput();
        }

        private void GetInput()
        {

            if (Vector2.Distance(GameManager.Instance.player.transform.position, gameObject.transform.position) < _minDistance)
                return;

            if (GameManager.Instance.player.currentRoom != _enemyBase._enemyRoom)
                return;

            Chase();

        }

        private void Chase()
        {
            transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.player.transform.position, _moveSpeed * Time.deltaTime);
        }
    }
}