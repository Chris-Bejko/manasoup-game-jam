using Manasoup.Interfaces;
using UnityEngine;

namespace Manasoup.AI
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public int Health { get; set; }

        public int MaxHealth;
        [SerializeField]
        public int _enemyRoom;
        [SerializeField]
        Rigidbody2D _thisRigidbody;
        public bool isDead;

        public virtual void Init()
        {
            Health = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Debug.LogError(Health);
            if (Health <= 0)
            {
                isDead = true;
                Die();
                GameManager.Instance.enemiesManager.CheckEnemiesLeft();
            }
        }

        public void Die()
        {
            ///Play dead anim
            gameObject.SetActive(false);
        }
    }
}