using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private CharacteristicManager _characteristicManager;
    
    [SerializeField]
    private Characteristic[] _characteristics;

    private void Awake()
    {
        _characteristicManager.Initialize(_characteristics);
    }
}
