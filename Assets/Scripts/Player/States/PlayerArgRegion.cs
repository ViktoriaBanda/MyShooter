using System;
using UnityEngine;

public class PlayerArgRegion : MonoBehaviour
{
    public event Action OnEnemyGetIntoArgRegion;
    public event Action<GameObject> OnEnemyInArgRegion;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(GlobalConstants.ENEMY_TAG))
        {
            OnEnemyGetIntoArgRegion?.Invoke();
        }
    }
    
    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag(GlobalConstants.ENEMY_TAG))
        {
            OnEnemyInArgRegion?.Invoke(collider.gameObject);
        }
    }
}