using System.Collections;
using System.Collections.Generic;
using SimpleEventBus;
using SimpleEventBus.Interfaces;
using UnityEngine;

public class EventStreams : MonoBehaviour
{
    public static IEventBus Game { get; } = new EventBus();
}