using System;
using Enemies;
using UnityEngine;

public class AgrRegion : MonoBehaviour
{
    public event Action<Zombie> OnEnemyGetIntoAgrRegion;
    public event Action<Zombie> OnEnemyGetOutAgrRegion;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(GlobalConstants.ENEMY_TAG))
        {
            OnEnemyGetIntoAgrRegion?.Invoke(collider.gameObject.GetComponent<Zombie>());
        }
    }
    
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag(GlobalConstants.ENEMY_TAG))
        {
            OnEnemyGetOutAgrRegion?.Invoke(collider.gameObject.GetComponent<Zombie>());
        }
    }
}