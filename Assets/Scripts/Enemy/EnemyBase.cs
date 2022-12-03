using Manasoup;
using Manasoup.Interfaces;
using UnityEngine;

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
        if(Health <= 0)
        {
            isDead = true;
            GameManager.Instance.enemiesManager.CheckEnemiesLeft();
        }
    }
}
