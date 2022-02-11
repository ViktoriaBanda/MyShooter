using System;
using UnityEngine;

public class BombAgrRegion : MonoBehaviour
{
    public event Action<GameObject> OnEnemyGetIntoAgrRegion;
    public event Action<GameObject> OnEnemyGetOutAgrRegion;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(GlobalConstants.ENEMY_TAG))
        {
            OnEnemyGetIntoAgrRegion?.Invoke(collider.gameObject);
        }
    }
    
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag(GlobalConstants.ENEMY_TAG))
        {
            OnEnemyGetOutAgrRegion?.Invoke(collider.gameObject);
        }
    }

}
