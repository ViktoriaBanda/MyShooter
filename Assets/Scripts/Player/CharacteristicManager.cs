using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacteristicManager", menuName = "CharacteristicManager")]
public class CharacteristicManager : ScriptableObject
{
    private Dictionary<CharacteristicType, Characteristic> _characteristics = new();

    public void Initialize(Characteristic[] characteristics)
    {
        foreach (var characteristic in characteristics)
        {
            _characteristics[characteristic.GetType()] = characteristic;
        }
    }

    public Characteristic GetCharacteristicByType(CharacteristicType type)
    {
        return _characteristics[type];
    }
}
