using System;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public string Name { get; set; }
    
    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            EventStreams.Game.Publish(new BuffAchieveEvent(this));
        }
    }

    private void Update()
    {
        gameObject.transform.RotateAround(transform.localPosition, Vector3.up, 1);
    }
}
