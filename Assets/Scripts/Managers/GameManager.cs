using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Manasoup
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public static event Action<GameState> OnGameStateChanged;

        private GameState _currentGameState;
        private GameState _previousGameState;


        private void Awake()
        {
            Instance = this;
            _currentGameState = GameState.UI;
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