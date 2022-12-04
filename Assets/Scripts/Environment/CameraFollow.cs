using Manasoup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;

    void Update()
    {
        transform.position = GameManager.Instance.player.transform.position + offset;
    }
}
