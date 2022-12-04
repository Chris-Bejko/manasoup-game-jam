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
            RoomTrigger.OnRoomChanged += ToggleEnemyHealth;
            CharacterCombat.DamageDone += UpdateValues;
            EnemyManager.OnCharacterKilled += ToggleEnemyHealth;
        }

        private void OnDisable()
        {
            EnemyManager.OnCharacterKilled -= ToggleEnemyHealth;
            RoomTrigger.OnRoomChanged -= ToggleEnemyHealth;
            CharacterCombat.DamageDone -= UpdateValues;
        }

        private void ToggleEnemyHealth(int room)
        {
            Debug.LogError("Player entered room");
            if (room == 0)
            {
                enemySlider.gameObject.SetActive(false);
                return;
            }

            currentEnemy = GameManager.Instance.enemiesManager.GetEnemyByRoom(room);
            if (currentEnemy == null)
                return;

            enemySlider.gameObject.SetActive(true);
            enemySlider.value = enemySlider.maxValue;
        }

        private void UpdateValues()
        {
            playerSlider.value = GameManager.Instance.player.Health;
            enemySlider.value = currentEnemy.Health;
        }
    }
}