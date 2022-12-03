using UnityEngine;
using Manasoup.Interfaces;
namespace Manasoup.AI
{
    public class EnemyMovement : MonoBehaviour, IDamageable
    {
        public int Health { get; set; }
        [SerializeField]
        public int _enemyRoom;

        [SerializeField]
        private float _minDistance;
        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private Vector3 _combatDistance;

        [SerializeField]
        Rigidbody2D _thisRigidbody;

        bool _canChase;
        private void OnEnable()
        {
            transform.GetChild(0).GetComponent<RotationCheck>().wallHit += Rotate;
        }

        private void OnDisable()
        {
            transform.GetChild(0).GetComponent<RotationCheck>().wallHit -= Rotate;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GetInput();
        }

        private void GetInput()
        {

            if (Vector2.Distance(GameManager.Instance.player.transform.position, gameObject.transform.position) > _minDistance)
            {
                Chase();
            }
        }
        private void Patrol()
        {
            transform.position += transform.up * Time.deltaTime * _moveSpeed;
        }
        private void Chase()
        {
            transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.player.transform.position, _moveSpeed * Time.deltaTime);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public void Rotate()
        {
            transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90);
        }

        public void OnPlayerEnteredRoom(int room)
        {
            _canChase = room == _enemyRoom;
        }

    }
}