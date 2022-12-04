using UnityEngine.UI;
using Manasoup.Character;

namespace Manasoup.UI
{
    public class InGame : UIScreenBase
    {

        Slider playerSlider;
        Slider enemySlider;

        CharacterBase currentEnemy;        

        private void OnEnable()
        {
            RoomTrigger.OnRoomEntered += EnableEnemyHealth;
            CharacterCombat.DamageDone += UpdateValues;
        }

        private void OnDisable()
        {
            
        }

        private void EnableEnemyHealth(int room)
        {
            currentEnemy = GameManager.Instance.enemiesManager.GetEnemyByRoom(room);
            if (currentEnemy == null)
                return;

            enemySlider.gameObject.SetActive(false);
            enemySlider.value = enemySlider.maxValue;
        }

        private void UpdateValues()
        {
            playerSlider.value = GameManager.Instance.player.Health;
            enemySlider.value = currentEnemy.Health;
        }
    }
}