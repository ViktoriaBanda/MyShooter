using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] 
    private GameObject _player;

    private float _speed;

    private void Awake()
    {
        _speed = _player.GetComponent<Player>().Speed;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _player.transform.Rotate(0, -1, 0);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            _player.transform.Rotate(0, 1, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            EventStreams.Game.Publish(new PlayerMovingEvent());
            
            _player.transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            return;
        }

        if (Input.GetKey(KeyCode.S))
        {
            EventStreams.Game.Publish(new PlayerMovingEvent());
            _player.transform.Translate(Vector3.back * Time.deltaTime * _speed);
            return;
        }
        
        EventStreams.Game.Publish(new PlayerStoppedEvent());
    }
}
