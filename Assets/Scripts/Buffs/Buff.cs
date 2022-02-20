using System;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public CharacteristicType Type { get; set; }

    public event Action BuffAchieveEvent;
    
    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            BuffAchieveEvent?.Invoke();
            EventStreams.Game.Publish(new BuffAchieveEvent(this));
        }
    }

    private void Update()
    {
        gameObject.transform.RotateAround(transform.position, Vector3.up, 1);
    }
}
