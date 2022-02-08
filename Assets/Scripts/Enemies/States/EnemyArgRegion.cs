using System;
using UnityEngine;

public class EnemyArgRegion : MonoBehaviour
{
    public event Action OnPlayerGetIntoArgRegion;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            OnPlayerGetIntoArgRegion?.Invoke();
        }
    }
}
