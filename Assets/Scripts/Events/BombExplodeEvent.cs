using SimpleEventBus.Events;
using UnityEngine;

public class BombExplodeEvent : EventBase
{
    public GameObject Bomb { get; }
    public BombExplodeEvent(GameObject bomb)
    {
        Bomb = bomb;
    }
}