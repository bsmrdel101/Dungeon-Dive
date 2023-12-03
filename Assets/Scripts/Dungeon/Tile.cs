using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Debug")]
    public float G;
    public float H;
    public float F => G + H;
    [HideInInspector] public bool IsWalkable;


    private void Start()
    {
        IsWalkable = GetComponent<Surface>().IsWalkable;
    }
}
