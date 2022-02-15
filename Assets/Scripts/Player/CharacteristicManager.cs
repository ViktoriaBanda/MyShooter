using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacteristicManager", menuName = "CharacteristicManager")]
public class CharacteristicManager : ScriptableObject
{
    //private Dictionary<string, Characteristic> _characteristics = new();
    private Dictionary<CharacteristicType, Characteristic> _characteristics = new();

    public void Initialize(Characteristic[] characteristics)
    {
        foreach (var characteristic in characteristics)
        {
            //_characteristics[characteristic.GetName()] = characteristic;
            _characteristics[characteristic.GetType()] = characteristic;
        }
    }
    
    //public Characteristic GetCharacteristicByName(string name)
    //{
    //    return _characteristics[name];
    //}
    
    public Characteristic GetCharacteristicByType(CharacteristicType type)
    {
        return _characteristics[type];
    }
}
