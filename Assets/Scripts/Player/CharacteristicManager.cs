using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacteristicManager", menuName = "CharacteristicManager")]
public class CharacteristicManager : ScriptableObject
{
    private Dictionary<string, Characteristic> _characteristics = new();

    public void Initialize(Characteristic[] characteristics)
    {
        foreach (var characteristic in characteristics)
        {
            _characteristics[characteristic.GetName()] = characteristic;
        }
    }
    
    public Characteristic GetCharacteristicByName(string name)
    {
        return _characteristics[name];
    }
}
