using UnityEngine.UI;
using Manasoup.Character;
using UnityEngine;

namespace Manasoup.UI
{
    public class InGame : UIScreenBase
    {
        [SerializeField]
        Slider playerSlider;
        [SerializeField]
        Slider enemySlider;

        CharacterBase currentEnemy;

        private void OnEnable()
        {
            playerSlider.value = playerSlider.maxValue;
            playerSlider.gameObject.SetActive(true);
            enemySlider.gameObject.SetActive(false);
            RoomTrigger.OnRoomChanged += ToggleEnemyHealth;
            EnemyManager.OnCharacterKilled += ToggleEnemyHealth;
            CharacterCombat.DamageDone += UpdateValues;
            CharacterBase.HealingDone += UpdateValues;
        }

        private void OnDisable()
        {
            EnemyManager.OnCharacterKilled -= ToggleEnemyHealth;
            RoomTrigger.OnRoomChanged -= ToggleEnemyHealth;
            CharacterCombat.DamageDone -= UpdateValues;
            CharacterBase.HealingDone -= UpdateValues;
        }

        private void ToggleEnemyHealth(int room)
        {
            if (room == GameManager.Instance.RoomCount)
            {
                GameManager.Instance.ChangeState(GameManager.GameState.Won);
                return;
            }
            if (room == 0)
            {
                enemySlider.gameObject.SetActive(false);
                return;
            }

            currentEnemy = GameManager.Instance.enemiesManager.GetEnemyByRoom(room);
            if (currentEnemy == null)
                return;


            enemySlider.maxValue = GameManager.Instance.enemiesManager.GetEnemyByRoom(room).MaxHealth;
            enemySlider.gameObject.SetActive(true);
            enemySlider.value = enemySlider.maxValue;
        }

        private void UpdateValues()
        {
            playerSlider.value = GameManager.Instance.player.Health;
            enemySlider.value = currentEnemy.Health;
        }

        public void OnGameStateChange(GameManager.GameState state)
        {
            if (state == GameManager.GameState.Lost)
            {
                enemySlider.gameObject.SetActive(false);
                playerSlider.gameObject.SetActive(false);
            }
        }
    }
}