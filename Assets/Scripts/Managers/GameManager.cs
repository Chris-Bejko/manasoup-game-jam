using System;
using UnityEngine;
using Manasoup.UI;
using Manasoup.Character;
using UnityEngine.UI;

namespace Manasoup
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public CharacterBase player;
        public UIManager uIManager;
        public EnemyManager enemiesManager;
        public static event Action<GameState> OnGameStateChanged;

        public Collider2D BossCollider;
        [SerializeField]
        private GameState _currentGameState;
        public int RoomCount;
        private void Awake()
        {
            uIManager.Init();
            Instance = this;
            _currentGameState = GameState.Unknown;
        }
        private void OnEnable()
        {

            CharacterBase.OnBossDied += OnBossDied;
        }
        private void OnDisable()
        {
            CharacterBase.OnBossDied -= OnBossDied;
        }
        private void Start()
        {
            ChangeState(GameState.UI);
        }
        public GameState GetCurrentState()
        {
            return _currentGameState;
        }

        public void ChangeState(GameState _newState)
        {
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
            uIManager.ChangeScreen(UIScreenID.MainMenu);
        }
        public void StatePlaying()
        {
            var img = uIManager.fadePanel.GetComponent<Image>();
            uIManager.fadePanel.GetComponent<Image>().color = new Color(img.color.a, img.color.b, img.color.g, 0);
        }


        public void StateWon()
        {
            uIManager.FadePanel();
            //uIManager.ChangeScreen(UIScreenID.WonMenu);
        }

        public void StateLost()
        {
            uIManager.FadePanel();
            uIManager.ChangeScreen(UIScreenID.LostMenu);
        }

        public void OnBossDied()
        {
            BossCollider.gameObject.SetActive(false);
        }
        public enum GameState
        {
            Unknown,
            UI,
            Playing,
            Won,
            Lost,
        }

    }
}

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}


[System.Serializable]
public class DirectionToTransform
{
    public Direction dir;
    public Transform attackPoint;
}