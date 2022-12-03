using Manasoup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected bool isMoving;
    protected bool isCombating;
}

public enum PlayerDirection
{
    Left,
    Right,
    Up,
    Down
}