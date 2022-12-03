using System;
using UnityEngine;
using Manasoup.UI;

namespace Manasoup
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public PlayerBase player;
        public UIManager uIManager;
        public static event Action<GameState> OnGameStateChanged;

        private GameState _currentGameState;
        private GameState _previousGameState;


        private void Awake()
        {
            uIManager.Init();
            Instance = this;
            _currentGameState = GameState.UI;
            ChangeState(GameState.Playing);
        }

        public GameState GetCurrentState()
        {
            return _currentGameState;
        }

        public void ChangeState(GameState _newState)
        {
            _previousGameState = _currentGameState;
            _currentGameState = _newState;
            OnGameStateChanged?.Invoke(_currentGameState);
        }
    }


    public enum GameState
    {
        Unknown,
        UI,
        Playing
    }

}