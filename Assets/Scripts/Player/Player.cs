using System;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Characteristic[] Characteristics;

    public float GetSpeed()
    {
        var speed = Characteristics.FirstOrDefault(characteristic => characteristic.Name == "Speed");
        return speed.CurrentValue;
    }
    
    public float GetMaxHealth()
    {
        var health = Characteristics.FirstOrDefault(characteristic => characteristic.Name == "Health");
        return health.MaxValue;
    }
    
    public float GetCurrentHealth()
    {
        var health = Characteristics.FirstOrDefault(characteristic => characteristic.Name == "Health");
        return health.CurrentValue;
    }

    public void SetHealth(float value)
    {
        var health = Characteristics.FirstOrDefault(characteristic => characteristic.Name == "Health");
        health.CurrentValue = value;
    }
}
