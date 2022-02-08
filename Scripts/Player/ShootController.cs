using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField]
    private float _reloadTimer = 0.2f;
    private float _currentTimer;
    private void Update()
    {
        if (_currentTimer > 0)
        {
            _currentTimer -= Time.deltaTime;
            return;
        }
        
        if (Input.GetMouseButton(0))
        {
            _currentTimer = _reloadTimer;
            EventStreams.Game.Publish(new PlayerShootingEvent());
        }
        else
        {
            EventStreams.Game.Publish(new PlayerStopShootingEvent());
        }
    }
}
