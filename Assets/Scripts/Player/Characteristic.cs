using System;
using UnityEngine;

[Serializable]
public class Characteristic
{
    [SerializeField] private string _name;
    [SerializeField] private CharacteristicType _type;
    [SerializeField] private float _maxValue;
    [SerializeField] private float _currentValue;

    public string GetName()
    {
        return _name;
    }

    public CharacteristicType GetType()
    {
        return _type;
    }
    public float GetMaxValue()
    {
        return _maxValue;
    }

    public float GetCurrentValue()
    {
        return _currentValue;
    }

    public void SetValue(float value)
    {
        _currentValue = value;
    }
}
