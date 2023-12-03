using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        if (DatabaseManager.Save == null) DatabaseManager.LoadData();
    }
}
