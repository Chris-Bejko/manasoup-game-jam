using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Manasoup.AI
{
    public class EnemyManager : MonoBehaviour
    {

        public List<EnemyBase> _enemies;

        void OnGameStateChanged(GameManager.GameState state)
        {
            if (state == GameManager.GameState.Playing)
                InitEnemies();
        }

        void InitEnemies()
        {
            _enemies = FindObjectsOfType<EnemyBase>().ToList();
            foreach(var e in _enemies)
            {
                e.Init();
            }

        }

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += OnGameStateChanged;
        }
        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }
        
        public void CheckEnemiesLeft()
        {
            int i = 0;
            foreach(var e in _enemies)
            {
                if (e.isDead)
                    i++;
            }
            if (i == _enemies.Count)
                Debug.LogError("All Enemies are dead");
        }
    }
}