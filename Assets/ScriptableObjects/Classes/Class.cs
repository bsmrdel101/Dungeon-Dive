using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Class", menuName = "ScriptableObjects/Class", order = 1)]
public class Class : ScriptableObject
{
    public string Name;
    public int BaseHp;
    public int BaseStrength;
    public int BaseAgility;
}
