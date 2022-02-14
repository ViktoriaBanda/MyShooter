using System;
using UnityEngine;

public class EnemyAgrRegion : MonoBehaviour
{
    public event Action OnPlayerGetIntoAgrRegion;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            OnPlayerGetIntoAgrRegion?.Invoke();
        }
    }
}
