using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Manasoup.Character
{
    public class EnemyManager : MonoBehaviour
    {

        public List<CharacterBase> _enemies;
        CharacterBase _player;

        public static event Action<int> OnCharacterKilled;
        private void Awake()
        {
            _enemies = FindObjectsOfType<CharacterBase>().ToList();
        }
        void OnGameStateChanged(GameManager.GameState state)
        {
            if (state == GameManager.GameState.Playing)
                InitEnemies();
        }

        void InitEnemies()
        {
            foreach(var e in _enemies)
            {
                e.gameObject.SetActive(true);
                e.Init();
                if (e._isPlayer)
                    _player = e;
            }
            if (_player != null)
                _enemies.Remove(_player);
            _player.gameObject.SetActive(true);
            _player.Init();

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

            OnCharacterKilled?.Invoke(0);
            int i = 0;
            foreach(var e in _enemies)
            {
                if (e._isDead)
                    i++;
            }
            if (i == _enemies.Count)
                Debug.Log("All Enemies are dead");
        }

        public CharacterBase GetEnemyByRoom(int room)
        {
            foreach(var e in _enemies)
            {
                if (e._currentRoom == room)
                    return e;
            }
            return null;
        }

        public void SetVolume(float volume)
        {
            foreach(var e in _enemies)
            {
                e.GetComponent<CharacterCombat>().SetVolume(volume);
            }
        }
    }
}