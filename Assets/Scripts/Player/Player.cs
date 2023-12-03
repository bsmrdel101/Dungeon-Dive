using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    public int MaxHp = 0;
    public int Hp = 0;
    public Class PlayerClass = null;

    public Player(Class playerClass)
    {
        PlayerClass = playerClass;
        MaxHp = playerClass.BaseHp;
        Hp = MaxHp;
    }
}
