using System;
using UnityEngine;
using Manasoup.UI;
using System.Collections.Generic;
using Manasoup.AI;

namespace Manasoup
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public PlayerBase player;
        public UIManager uIManager;
        public EnemyManager enemiesManager;
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
            HandleGameStateChange(_currentGameState);
        }

        public void HandleGameStateChange(GameState state)
        {
            switch (state)
            {
                case GameState.Unknown:
                    break;
                case GameState.UI:
                    StateUI();
                    break;
                case GameState.Playing:
                    StatePlaying();
                    break;
                case GameState.Boss:
                    StateBoss();
                    break;
                case GameState.Won:
                    StateWon();
                    break;
                case GameState.Lost:
                    StateLost();
                    break;

            }

            OnGameStateChanged?.Invoke(_currentGameState);

        }

        ///GameState Handlers
        public void StateUI()
        {

        }

        public void StatePlaying()
        {

        }

        public void StateBoss()
        {

        }

        public void StateWon()
        {

        }

        public void StateLost()
        {

        }


        public enum GameState
        {
            Unknown,
            UI,
            Playing,
            Boss,
            Won,
            Lost
        }

    }
}