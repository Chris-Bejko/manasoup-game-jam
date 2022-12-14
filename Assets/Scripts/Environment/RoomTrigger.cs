using Manasoup;
using System;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public int roomID;
    public static event Action<int> OnRoomChanged;
    
    bool enteredOnce;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        GameManager.Instance.player._currentRoom = roomID;

        if(roomID == GameManager.Instance.RoomCount)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Won);
            return;
        }
        if (GameManager.Instance.enemiesManager.GetEnemyByRoom(roomID) == null)
            return;

        if (GameManager.Instance.enemiesManager.GetEnemyByRoom(roomID)._isDead)
            return;

        if (!enteredOnce)
        {   
            enteredOnce = true;
            OnRoomChanged?.Invoke(roomID);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        GameManager.Instance.player._currentRoom = 0;
        enteredOnce = false;
        OnRoomChanged?.Invoke(0);
    }
}
