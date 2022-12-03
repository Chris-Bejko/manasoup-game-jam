using Manasoup;
using Manasoup.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Manasoup
{
    public class PlayerBase : MonoBehaviour, IDamageable
    {
        protected bool isMoving;
        protected bool isCombating;
        public int currentRoom;

        public int Health { get; set; }

        public int MaxHealth;

        public void Init()
        {
            Health = MaxHealth;
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
                GameManager.Instance.ChangeState(GameManager.GameState.Lost);
            }
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