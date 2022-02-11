using System;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public string Name { get; set; }
    
    [SerializeField]
    protected AudioSource _audioSource;
    
    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            EventStreams.Game.Publish(new BuffAchieveEvent(this, _audioSource));
        }
    }

    private void Update()
    {
        gameObject.transform.RotateAround(transform.localPosition, Vector3.up, 1);
    }
}
