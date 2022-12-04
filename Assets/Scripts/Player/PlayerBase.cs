using Manasoup;
using Manasoup.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Manasoup
{
    public class PlayerBase : MonoBehaviour, IDamageable
    {
        public bool isMoving;
        public bool isCombating;
        public int currentRoom;
        public PlayerCombat _playerCombat;
        public PlayerMovement _playerMovement;
        public Animator _animator;
        public bool canAttackWhileShooting;

        public int Health { get; set; }

        public int MaxHealth;

        public void Init()
        {
            Health = MaxHealth;
            _playerCombat = GetComponent<PlayerCombat>();  
            _playerMovement = GetComponent<PlayerMovement>();  
        }
        public void ChangeRoom(int newRoom)
        {
            currentRoom = newRoom;
            //OnRoomChanged?.Invoke(newRoom); An to xreiastoume
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if(Health <= 0)
            {
                Health = 0;
                Die();
                GameManager.Instance.ChangeState(GameManager.GameState.Lost);
            }
        }

        public void Die()
        {

        }
    }

    public enum PlayerDirection
    {
        Left,
        Right,
        Up,
        Down
    }
}