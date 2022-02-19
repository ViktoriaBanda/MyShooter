using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSettings 
{
    public string Name;
    public Sprite Icon;
    public GameObject Prefab;
    public Characteristic[] Characteristics;
}
