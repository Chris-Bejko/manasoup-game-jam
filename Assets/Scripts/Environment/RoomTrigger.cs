using Manasoup;
using System;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public int roomID;
    public static event Action<int> OnRoomEntered;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        GameManager.Instance.player._currentRoom = roomID;
        OnRoomEntered?.Invoke(roomID);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        GameManager.Instance.player._currentRoom = 0;
    }
}
