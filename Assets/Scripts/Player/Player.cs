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
        return health.CurrentValue;
    }
}
