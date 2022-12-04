using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Manasoup.Character;

namespace Manasoup.AI
{
    public class EnemyManager : MonoBehaviour
    {

        public List<CharacterBase> _enemies;

        void OnGameStateChanged(GameManager.GameState state)
        {
            if (state == GameManager.GameState.Playing)
                InitEnemies();
        }

        void InitEnemies()
        {
            CharacterBase player = null;
            _enemies = FindObjectsOfType<CharacterBase>().ToList();
            foreach(var e in _enemies)
            {
                e.Init();
                if (e._isPlayer)
                    player = e;
            }
            if (player != null)
                _enemies.Remove(player);

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
                if (e._isDead)
                    i++;
            }
            if (i == _enemies.Count)
                Debug.LogError("All Enemies are dead");
        }
    }
}