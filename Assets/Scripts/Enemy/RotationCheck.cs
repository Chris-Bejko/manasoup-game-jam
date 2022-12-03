using System;
using UnityEngine;


namespace Manasoup.AI
{
    public class RotationCheck : MonoBehaviour
    {

        public event Action wallHit;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Walls"))
            {
                wallHit?.Invoke();
            }

        }
    }
}